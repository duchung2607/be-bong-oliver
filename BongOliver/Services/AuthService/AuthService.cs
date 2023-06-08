using System.Security.Cryptography;
using System.Text;
// using BongOliver.API.Database;
using BongOliver.Models;
using BongOliver.DTOs.User;
using BongOliver.Services.TokenService;
using BongOliver.Repositories.UserRepository;
using BongOliver.Repositories.WaletRepository;
using BongOliver.DTOs.Response;
using BongOliver.Services.RoleService;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using BongOliver.Services.EmailService;

namespace BongOliver.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IWaletRepository _waletRepository;
        private readonly IRoleService _roleService;
        private readonly IEmailService _emailService;

        private readonly IConfiguration _config;

        public AuthService(IUserRepository userRepository, ITokenService tokenService, IConfiguration config, IEmailService emailService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _config = config;
            _emailService = emailService;
        }

        public ResponseDTO Login(AuthUserDTO authUserDto)
        {
            authUserDto.username = authUserDto.username.ToLower();

            var currentUser = _userRepository.GetUserByUsername(authUserDto.username);

            if (currentUser == null)
            {
                return new ResponseDTO()
                {
                    code = 400,
                    message = "Username không tồn tại!",
                    data = null
                };
            }

            using var hmac = new HMACSHA512(currentUser.PasswordSalt);
            var passwordBytes = hmac.ComputeHash(
                Encoding.UTF8.GetBytes(authUserDto.password)
            );

            for (int i = 0; i < currentUser.PasswordHash.Length; i++)
            {
                if (currentUser.PasswordHash[i] != passwordBytes[i])
                {
                    return new ResponseDTO()
                    {
                        code = 400,
                        message = "Mật khẩu không chính xác!",
                        data = null
                    };
                }
            }

            return new ResponseDTO()
            {
                code = 200,
                message = "Login success",
                data = _tokenService.CreateToken(currentUser)
            };
        }

        public ResponseDTO Register(RegisterUserDTO registerUserDto)
        {
            registerUserDto.username = registerUserDto.username.ToLower();
            var currentUser = _userRepository.GetUserByUsername(registerUserDto.username);
            if (currentUser != null)
            {
                return new ResponseDTO()
                {
                    code = 400,
                    message = "Username đã tồn tại!"
                };
            }

            if(_userRepository.GetUserByEmail(registerUserDto.email) != null)
                return new ResponseDTO()
                {
                    code = 400,
                    message = "Email đã tồn tại!"
                };

            if (registerUserDto.password != registerUserDto.cfpassword)
            {
                return new ResponseDTO()
                {
                    code = 400,
                    message = "Confirm password was wrong!"
                };
            }

            using var hmac = new HMACSHA512();
            var passwordBytes = Encoding.UTF8.GetBytes(registerUserDto.password);

            var newWalet = new Walet()
            {
                money = 0
            };

            //_waletRepository.CreateWalet(newWalet);
            string code = Guid.NewGuid().ToString("N").Substring(0, 6);

            var newUser = new User()
            {
                name = registerUserDto.name,
                phone = "",
                email = registerUserDto.email,
                gender = true,
                username = registerUserDto.username,
                token = code,
                roleId = 3,
                Walet = newWalet,
                //waletId = _waletRepository.GetWalet(1).id,
                PasswordSalt = hmac.Key,
                PasswordHash = hmac.ComputeHash(passwordBytes)
            };

            _userRepository.CreateUser(newUser);
            if (_userRepository.IsSaveChanges())
                    return new ResponseDTO()
                    {
                        message = "Đăng ký thành công!",
                        data = _tokenService.CreateToken(newUser)
                    };
            else return new ResponseDTO() { code = 400, message = "Đăng ký thất bại!" };
        }

        public ResponseDTO VerifyEmail(string email, string token)
        {
            try
            {
                var user = _userRepository.GetUserByEmail(email);
                if (user != null && user.token == token)
                {
                    user.isVerify = true;
                    _userRepository.UpdateUser(user);
                    if(_userRepository.IsSaveChanges())
                    return new ResponseDTO()
                    {
                        code = 200,
                        message = "Success! Your email is verify!",
                        data = null
                    };
                    else return new ResponseDTO()
                    {
                        code = 400,
                        message = "Faile! Your email is not verify!",
                    };
                }

                else
                    return new ResponseDTO()
                    {
                        code = 400,
                        message = "Faile",
                        data = null
                    };
            }
            catch (Exception e)
            {
                return new ResponseDTO()
                {
                    code = 400,
                    message = "Faile. " + e.Message,
                    data = null
                };
            }
        }
        public ResponseDTO ForgotPassword(string email)
        {
            var user = _userRepository.GetUserByEmail(email);
            if (user == null) return new ResponseDTO()
            {
                code = 400,
                message = "Email is not valid"
            };

            string code = Guid.NewGuid().ToString("N").Substring(0, 10);

            using var hmac = new HMACSHA512();
            var newPass = Encoding.UTF8.GetBytes(code);

            user.PasswordSalt = hmac.Key;
            user.PasswordHash = hmac.ComputeHash(newPass);

            _userRepository.UpdateUser(user);
            if (_userRepository.IsSaveChanges())
            {
                return _emailService.SendEmail(user.email, "Forgot password", "New pass word for your account is : " + code);
            }
            else return new ResponseDTO()
            {
                code = 400,
                message = "Faile",
                data = null
            };
        }

    }
}
