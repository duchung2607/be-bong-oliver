using AutoMapper;
using BongOliver.DTOs.Response;
using BongOliver.DTOs.User;
using BongOliver.Models;
using BongOliver.Repositories.BookingRepository;
using BongOliver.Repositories.PaymentRepository;
using BongOliver.Repositories.UserRepository;
using BongOliver.Services.BookingService;
using BongOliver.Services.EmailService;
using BongOliver.Services.VnPayService;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace BongOliver.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IVnPayService _vnPayService;
        private readonly IBookingRepository _bookingRepository;
        private readonly IPaymentRepository _paymentRepository;
        public UserService(IUserRepository userRepository, IMapper mapper, IEmailService emailService
            , IVnPayService vnPayService, IBookingRepository bookingRepository
            , IPaymentRepository paymentRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _emailService = emailService;
            _vnPayService = vnPayService;
            _bookingRepository = bookingRepository;
            _paymentRepository = paymentRepository;
        }

        public ResponseDTO ChangePass(ChangePassDTO changePassDTO, string username)
        {
            var user = _userRepository.GetUserByUsername(username);
            if (user == null) return new ResponseDTO() { code = 400, message = "Username is not valid" };

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var passwordBytes = hmac.ComputeHash(
                Encoding.UTF8.GetBytes(changePassDTO.currentPass)
            );

            for (int i = 0; i < user.PasswordHash.Length; i++)
            {
                if (user.PasswordHash[i] != passwordBytes[i])
                {
                    return new ResponseDTO()
                    {
                        code = 400,
                        message = "Password is invalid!",
                        data = null
                    };
                }
            }

            if (changePassDTO.newPass != changePassDTO.reNewPass)
                return new ResponseDTO()
                {
                    code = 400,
                    message = "Password must be equal Re Password!",
                    data = null
                };

            using var nhmac = new HMACSHA512();
            var newPass = Encoding.UTF8.GetBytes(changePassDTO.newPass);

            user.PasswordSalt = nhmac.Key;
            user.PasswordHash = nhmac.ComputeHash(newPass);

            _userRepository.UpdateUser(user);
            if (_userRepository.IsSaveChanges()) return new ResponseDTO()
            {
                code = 200,
                message = "Success",
                data = null
            };
            else return new ResponseDTO()
            {
                code = 400,
                message = "Faile",
                data = null
            };
        }

        public UserDTO ConvertToDTO(User user)
        {
            return new UserDTO()
            {
                id = user.id,
                username = user.username,
                name = user.name,
                email = user.email,
                phone = user.phone,
                avatar = user.avatar,
                gender = user.gender,
                rank = user.rank,
                create = user.create,
                update = user.update,
                isDelete = user.isDelete,
                isVerify = user.isVerify,
                walet = new DTOs.Walet.WaletDTO()
                {
                    id = user.Walet.id,
                    money = user.Walet.money
                },
                role = new DTOs.Role.RoleDTO()
                {
                    id = user.role.id,
                    name = user.role.name,
                }
            };
        }

        public ResponseDTO CreateUser(CreateUserDTO createUser)
        {
            createUser.username = createUser.username.ToLower();
            var currentUser = _userRepository.GetUserByUsername(createUser.username);
            if (currentUser != null)
            {
                return new ResponseDTO()
                {
                    code = 400,
                    message = "Username đã tồn tại"
                };
            }

            if (!_userRepository.EmailIsValid(createUser.email)) return new ResponseDTO() { code = 400, message = "Email đã tồn tại" };

            string phonePattern = @"^0\d{9}$";
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if (!Regex.IsMatch(createUser.phone, phonePattern))
            {
                return new ResponseDTO() { code = 400, message = "Phone is not valid" };
            }

            if (!Regex.IsMatch(createUser.email, emailPattern))
            {
                return new ResponseDTO() { code = 400, message = "Email is not valid" };
            }


            using var hmac = new HMACSHA512();
            var passwordBytes = Encoding.UTF8.GetBytes(createUser.password);

            var newWalet = new Walet()
            {
                money = 0
            };
            string code = Guid.NewGuid().ToString("N").Substring(0, 6);
            var newUser = new User()
            {
                name = createUser.name,
                avatar = createUser.avatar,
                phone = createUser.phone,
                email = createUser.email,
                gender = createUser.gender,
                username = createUser.username,
                roleId = createUser.roleId,
                token = code,
                Walet = new Walet() { money = createUser.WaletDTO.money },
                PasswordSalt = hmac.Key,
                PasswordHash = hmac.ComputeHash(passwordBytes)
            };

            _userRepository.CreateUser(newUser);
            if (_userRepository.IsSaveChanges()) return new ResponseDTO();
            else return new ResponseDTO()
            {
                code = 400,
                message = "Faile"
            };
        }

        public ResponseDTO DeleteUser(string username)
        {
            var userDelete = _userRepository.GetUserByUsername(username);
            if (userDelete == null) return new ResponseDTO()
            {
                code = 404,
                message = "Username is not valid"
            };
            userDelete.isDelete = !userDelete.isDelete;
            _userRepository.UpdateUser(userDelete);
            //_userRepository.DeleteUser(userDelete);
            if (_userRepository.IsSaveChanges()) return new ResponseDTO();
            else return new ResponseDTO()
            {
                code = 400,
                message = "Faile"
            };
        }

        public bool EmailIsValid(string email)
        {
            var user = _userRepository.GetUserByEmail(email);
            if (user != null) return false;
            return true;
        }

        public ResponseDTO GetStylist()
        {
            List<UserDTO> userDTOs = new List<UserDTO>();
            var users = _userRepository.GetStylist();
            if (users != null)
                foreach (var user in users)
                {
                    userDTOs.Add(ConvertToDTO(user));
                }
            return new ResponseDTO()
            {
                data = userDTOs
            };
        }

        public ResponseDTO GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO GetUserByIds(List<int> ids)
        {
            var userDTOs = new List<UserDTO>();
            foreach (var id in ids)
            {
                var user = _userRepository.GetUserById(id);
                if(user != null)
                userDTOs.Add(_mapper.Map<UserDTO>(user));
            }
            return new ResponseDTO()
            {
                data = userDTOs
            };
        }

        public ResponseDTO GetUserByUsername(string username)
        {
            var user = _userRepository.GetUserByUsername(username);
            if (user == null)
                return new ResponseDTO()
                {
                    message = "Khong co user nao co username " + username
                };
            return new ResponseDTO() { data = ConvertToDTO(user) };
        }

        public ResponseDTO GetUsers(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id")
        {
            var userDTOs = new List<UserDTO>();
            var users = _userRepository.GetUsers(page, pageSize, key, sortBy);
            if (users != null)
            {
                foreach (var user in users)
                {
                    userDTOs.Add(ConvertToDTO(user));
                }
                return new ResponseDTO()
                {
                    data = userDTOs,
                    total = _userRepository.GetTotal()
                };
            }
            return new ResponseDTO()
            {
                code = 404,
                message = "Faile",
            };
        }

        public bool IsSaveChanges()
        {
            return _userRepository.IsSaveChanges();
        }

        public ResponseDTO PayIn(string username, double money)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDTO> PayMentWithWalet(string username, int bookingId)
        {
            var user = _userRepository.GetUserByUsername(username);
            if (user == null) return new ResponseDTO()
            {
                code = 400,
                message = "User không tồn tại"
            };
            var booking = _bookingRepository.GetBookingById(bookingId);
            if (booking == null) return new ResponseDTO()
            {
                code = 400,
                message = "Booking không tồn tại"
            };
            if (booking.status == "done") return new ResponseDTO()
            {
                code = 400,
                message = "Booking đã hoàn thành"
            };
            if (booking.User.username != user.username) return new ResponseDTO()
            {
                code = 400,
                message = "Bạn không thể thanh toán booking của người khác"
            };

            double price = 0;
            foreach (Service service in booking.Services)
            {
                price += service.price;
            }

            if (user.Walet.money < price) return new ResponseDTO()
            {
                code = 400,
                message = "Không đủ số dư"
            };
            user.Walet.money -= price;
            _userRepository.UpdateUser(user);
            booking.status = "done";
            _bookingRepository.UpdateBooking(booking);

            if (_userRepository.IsSaveChanges())
            {
                var payment = new Payment();
                payment.bookingId = bookingId;
                payment.time = DateTime.Now;
                payment.total = price;
                payment.mode = "WALET";
                await _paymentRepository.CreatePaymentAsync(payment);
                await _paymentRepository.IsSaveChange();

                return new ResponseDTO()
                {
                    message = "Thanh toán thành công"
                };
            }    
            else
            return new ResponseDTO()
            {
                code = 400,
                message = "Thanh toán thất bại"
            };
        }

        public ResponseDTO UpdateUser(UserDTO user)
        {
            var currentUSer = _userRepository.GetUserByUsername(user.username);
            currentUSer.email = user.email;
            currentUSer.phone = user.phone;
            currentUSer.name = user.name;
            currentUSer.gender = user.gender;
            _userRepository.UpdateUser(currentUSer);
            if (_userRepository.IsSaveChanges()) return new ResponseDTO();
            else return new ResponseDTO() { code = 400, message = "Update faile" };
        }

        public ResponseDTO UpdateUser(UpdateUser user, string username)
        {
            var currentUSer = _userRepository.GetUserByUsername(username);
            if (currentUSer == null) return new ResponseDTO() { code = 400, message = "Username is not valid" };

            currentUSer.email = user.email.Trim() == "" ? currentUSer.email : user.email.Trim();
            currentUSer.phone = user.phone.Trim() == "" ? currentUSer.phone : user.phone.Trim();
            currentUSer.name = user.name.Trim() == "" ? currentUSer.name : user.name.Trim();
            currentUSer.avatar = user.avatar.Trim() == "" ? currentUSer.avatar : user.avatar.Trim();
            currentUSer.gender = user.gender;
            currentUSer.update = DateTime.Now;

            _userRepository.UpdateUser(currentUSer);
            if (_userRepository.IsSaveChanges()) return new ResponseDTO();
            else return new ResponseDTO() { code = 400, message = "Update faile" };
        }

        public ResponseDTO UpdateWaletUser(string username, double money)
        {
            var user = _userRepository.GetUserByUsername(username);

            if (user == null) return new ResponseDTO() { code = 400, message = "Username is not valid" };

            user.Walet.money = money;

            _userRepository.UpdateUser(user);
            if (_userRepository.IsSaveChanges()) return new ResponseDTO();
            else return new ResponseDTO() { code = 400, message = "Update faile" };
        }

        public ResponseDTO VerifyEmail(string username)
        {
            var user = _userRepository.GetUserByUsername(username);

            if (user == null) return new ResponseDTO() { code = 400, message = "Username is not valid" };
            if (user.isVerify) return new ResponseDTO() { code = 400, message = "Your email is verify" };

            return _emailService.SendEmail(user.email, "Verify your email", "Please click this link to verify: http://localhost:3000/verify-return?email=" + user.email.Split("@")[0] + "%40" + "gmail.com" + "&token=" + user.token); ;
        }
    }
}
