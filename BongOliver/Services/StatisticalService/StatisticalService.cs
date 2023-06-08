using AutoMapper;
using BongOliver.DTOs.Response;
using BongOliver.Repositories.BookingRepository;
using BongOliver.Repositories.UserRepository;
using BongOliver.Services.EmailService;
using BongOliver.Services.VnPayService;

namespace BongOliver.Services.StatisticalService
{
    public class StatisticalService : IStatisticalService
    {
        private readonly IUserRepository _userRepository;
        private readonly IBookingRepository _bookingRepository;
        public StatisticalService(IUserRepository userRepository, IBookingRepository bookingRepository)
        {
            _userRepository = userRepository;
            _bookingRepository = bookingRepository;
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
            foreach ( var user in users )
            {
                if(user.create.Year == year && user.role.name == "user")
                {
                    arrUser[user.create.Month-1] += 1;
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
            foreach (var booking in bookings)
            {
                if (booking.create.Year == year)
                {
                    foreach (var service in booking.Services)
                    {
                        arrBooking[booking.create.Month - 1] += service.price;
                    }
                    countBooking++;
                    if (booking.create.Month == month && booking.create.Day == day) newBooking++;
                }
            }
            return new ResponseDTO()
            {
                data = new
                {
                    statisticalRevenue = arrBooking,
                    volatilityRevenue = arrBooking[month - 2] != 0? (arrBooking[month - 1] - arrBooking[month - 2]) / arrBooking[month - 2] : 0,
                    totalBooking = countBooking,
                    newBooking = newBooking,
                }
            };
        }
    }
}
