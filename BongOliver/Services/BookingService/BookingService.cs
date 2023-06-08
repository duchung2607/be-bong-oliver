using AutoMapper;
using BongOliver.DTOs.Booking;
using BongOliver.DTOs.Response;
using BongOliver.Models;
using BongOliver.Repositories.BookingRepository;
using BongOliver.Repositories.ServiceRepository;
using BongOliver.Repositories.UserRepository;

namespace BongOliver.Services.BookingService
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public BookingService(IBookingRepository bookingRepository, IMapper mapper, IServiceRepository serviceRepository, IUserRepository userRepository)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _serviceRepository = serviceRepository;
            _userRepository = userRepository;
        }

        public async Task<ResponseDTO> DeleteBooking(int id)
        {
            var booking = _bookingRepository.GetBookingById(id);
            if (booking == null) return new ResponseDTO()
            {
                code = 404,
                message = "Khong tim thay booking voi id = " + id,
                data = null
            };
            _bookingRepository.DeleteBooking(booking);
            if (await IsSaveChanges())
                return new ResponseDTO()
                {
                    code = 200,
                    message = "Success",
                    data = null
                };
            else return new ResponseDTO()
            {
                code = 404,
                message = "Faile",
                data = null
            };
        }

        public double GetPriceBooking(List<Service> services)
        {
            double price = 0;
            foreach (Service service in services)
            {
                price += service.price;
            }
            return price;
        }
        public ResponseDTO GetAllBooking(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id")
        {
            List<BookingDTO> bookingDTOs = new List<BookingDTO>();
            var bookings = _bookingRepository.GetAllBooking( page, pageSize, key, sortBy);
            if (bookings == null) return new ResponseDTO();
            foreach (var booking in bookings)
            {
                double price = GetPriceBooking(booking.Services);
                bookingDTOs.Add(new BookingDTO()
                {
                    id = booking.id,
                    description = booking.description,
                    status = booking.status,
                    time = booking.time,
                    price = price,
                    create = booking.create,
                });
            }
            return new ResponseDTO()
            {
                data = bookingDTOs,
                total = _bookingRepository.GetTotal()
            };
        }

        public ResponseDTO GetBookingById(int id)
        {
            var booking = _bookingRepository.GetBookingById(id);

            if (booking == null) return new ResponseDTO()
            {
                code = 400,
                message = "Khong tim thay booking id = " + id,
                data = null
            };
            else
            {
                var bookingDTO = _mapper.Map<BookingDetailDTO>(booking);
                bookingDTO.ServiceBookingDTOs = new List<ServiceBookingDTO>();
                foreach(Service service in booking.Services)
                {
                    bookingDTO.ServiceBookingDTOs.Add(new ServiceBookingDTO()
                    {
                        id= service.id,
                        name = service.name,
                        description = service.description,
                        price = service.price,
                        image = service.image,
                    });
                }

                bookingDTO.usernameUser = booking.User.username;
                bookingDTO.usernameStylist = booking.Stylist.username;
                bookingDTO.price = GetPriceBooking(booking.Services);
                return new ResponseDTO()
                {
                    code = 200,
                    message = "Success",
                    data = bookingDTO
                };

            }
        }

        public ResponseDTO GetBookingByUser(string username, int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id")
        {
            var bookings = _bookingRepository.GetBookingByUser(username, page, pageSize, key, sortBy);
            List<BookingDTO> bookingDTOs = new List<BookingDTO>();
            if (bookings == null) return new ResponseDTO();
            foreach (var booking in bookings)
            {
                var bookingDTO = _mapper.Map<BookingDTO>(booking);
                bookingDTO.price = GetPriceBooking(booking.Services);
                bookingDTOs.Add(bookingDTO);
            }
            return new ResponseDTO()
            {
                data = bookingDTOs
            };
        }

        public ResponseDTO GetBookingByStylist(string username, int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id")
        {
            var bookings = _bookingRepository.GetBookingByStylist(username, page, pageSize, key, sortBy);
            List<BookingDTO> bookingDTOs = new List<BookingDTO>();
            if (bookings == null) return new ResponseDTO();
            foreach (var booking in bookings)
            {
                bookingDTOs.Add(_mapper.Map<BookingDTO>(booking));
            }
            return new ResponseDTO()
            {
                data = bookingDTOs
            };
        }

        public async Task<bool> IsSaveChanges()
        {
            return await _bookingRepository.IsSaveChanges();
        }

        public async Task<ResponseDTO> UpdateBooking(UpdateBookingDTO dto, int id)
        {
            var booking = _bookingRepository.GetBookingById(id);
            if (booking == null) return new ResponseDTO()
            {
                code = 404,
                message = "Khong tim thay booking voi id = " + id,
                data = null
            };
            booking.description = dto.description;
            booking.status = dto.status;
            booking.time = dto.time;
            booking.update = DateTime.Now;
            _bookingRepository.UpdateBooking(booking);
            if (await IsSaveChanges())
                return new ResponseDTO()
                {
                    code = 200,
                    message = "Success",
                    data = null
                };
            else return new ResponseDTO()
            {
                code = 404,
                message = "Faile",
                data = null
            };

        }

        public async Task<ResponseDTO> CreateBooking(CreateBookingDTO createBookingDTO)
        {
            var user = _userRepository.GetUserById(createBookingDTO.userId);
            if (user == null) return new ResponseDTO()
            {
                code = 404,
                message = "User not valid"
            };
            if (user.role.name != "user" && user.role.name != "guest") return new ResponseDTO()
            {
                code = 404,
                message = "user Id is must be User"
            };

            var stylist = _userRepository.GetUserById(createBookingDTO.stylistId);
            if (stylist == null) return new ResponseDTO()
            {
                code = 404,
                message = "Stylist not valid"
            };
            if (stylist.role.name != "stylist") return new ResponseDTO()
            {
                code = 404,
                message = "stylistId is must be Stylist"
            };

            var booking = new Booking();
            booking.time = createBookingDTO.time;
            booking.description=createBookingDTO.description;
            booking.status = createBookingDTO.status;
            booking.userId = createBookingDTO.userId;
            booking.stylistId = createBookingDTO.stylistId;

            booking.Services = new List<Service>();
            foreach(int id in createBookingDTO.serviceIds)
            {
                var service = _serviceRepository.GetServiceById(id);
                if (service != null) booking.Services.Add(service);
            }

            _bookingRepository.CreateBooking(booking);
            if (await IsSaveChanges()) return new ResponseDTO();
            else return new ResponseDTO()
            {
                code = 400,
                message = "faile"
            };

            throw new NotImplementedException();
        }

        public ResponseDTO GetSchedules(int stylistId, DateTime date)
        {
            var stylist = _userRepository.GetUserById(stylistId);
            if (stylist == null || stylist.role.name != "stylist") return new ResponseDTO()
            {
                message = "Stylist khong ton tai"
            };

            var res = _bookingRepository.GetSchedules(stylistId, date);
            return new ResponseDTO()
            {
                data = res
            };
        }

        public ResponseDTO GetCalendar()
        {
            var res = _bookingRepository.GetCalendar();
            return new ResponseDTO()
            {
                data = res
            };
        }
    }
}
