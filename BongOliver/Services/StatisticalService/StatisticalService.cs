using AutoMapper;
using BongOliver.DTOs.Response;
using BongOliver.Repositories.BookingRepository;
using BongOliver.Repositories.ServiceRepository;
using BongOliver.Repositories.UserRepository;
using BongOliver.Services.EmailService;
using BongOliver.Services.VnPayService;

namespace BongOliver.Services.StatisticalService
{
    public class StatisticalService : IStatisticalService
    {
        private readonly IUserRepository _userRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IServiceRepository _serviceRepository;
        public StatisticalService(IUserRepository userRepository, IBookingRepository bookingRepository, IServiceRepository serviceRepository)
        {
            _userRepository = userRepository;
            _bookingRepository = bookingRepository;
            _serviceRepository = serviceRepository;
        }
        public ResponseDTO StatisticalUser()
        {
            int[] arrUser = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            DateTime today = DateTime.Today;
            var year = today.Year;
            var month = today.Month;
            var day = today.Day;

            var users = _userRepository.GetUsers();
            int countUser = 0;
            foreach (var user in users)
            {
                if (user.create.Year == year && user.role.name == "user")
                {
                    arrUser[user.create.Month - 1] += 1;
                    countUser++;
                }
            }

            return new ResponseDTO()
            {
                data = new
                {
                    statisticalUser = arrUser,
                    volatilityUser = arrUser[month - 2] != 0 ? (arrUser[month - 1] - arrUser[month - 2]) / (double)arrUser[month - 2] : 0,
                    totalUser = countUser,
                }
            };
        }

        public ResponseDTO StatisticalRevenue()
        {
            double[] arrBooking = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            DateTime today = DateTime.Today;
            var year = today.Year;
            var month = today.Month;
            var day = today.Day;

            var bookings = _bookingRepository.GetAllBooking();
            int countBooking = 0;
            int newBooking = 0;
            double revenue = 0;
            foreach (var booking in bookings)
            {
                if (booking.create.Year == year && booking.status == "done")
                {
                    foreach (var service in booking.Services)
                    {
                        arrBooking[booking.create.Month - 1] += service.price;
                    }
                    if (booking.create.Month == month) countBooking++;
                    if (booking.create.Month == month && booking.create.Day == day) newBooking++;
                }
            }
            return new ResponseDTO()
            {
                data = new
                {
                    revenue = arrBooking[month - 1],
                    //arrBooking[0]+ arrBooking[1] + arrBooking[2] + arrBooking[3] + arrBooking[4] + arrBooking[5] + arrBooking[6] 
                    //+ arrBooking[7] + arrBooking[8] + arrBooking[9] + arrBooking[10] + arrBooking[11],
                    statisticalRevenue = arrBooking,
                    volatilityRevenue = arrBooking[month - 2] != 0 ? (arrBooking[month - 1] - arrBooking[month - 2]) / arrBooking[month - 2] : 0,
                    totalBooking = countBooking,
                    newBooking = newBooking,
                }
            };
        }

        public ResponseDTO StatisticalMostOfService()
        {
            var services = _serviceRepository.GetMostOfService(5);
            var data = new List<int>();
            foreach (var service in services)
            {
                data.Add(service.Bookings.Count);
            }
            return new ResponseDTO()
            {
                data = new
                {
                    names = services.Select(s => s.name),
                    count = data
                }
            };
        }
    }
}
