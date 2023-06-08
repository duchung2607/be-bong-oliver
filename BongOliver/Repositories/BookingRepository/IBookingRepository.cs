using BongOliver.Models;

namespace BongOliver.Repositories.BookingRepository
{
    public interface IBookingRepository
    {
        public List<Booking> GetAllBooking(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id");
        public List<Booking> GetAllBooking();
        public List<Booking> GetBookingByUser(string username, int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id");
        public List<Booking> GetBookingByStylist(string username, int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id");
        public List<DateTime> GetSchedules(int stylistId, DateTime date);
        public List<object> GetCalendar();
        public int GetTotal();
        public void DeleteBooking(Booking booking);
        public Booking GetBookingById(int id);
        public void UpdateBooking(Booking booking);
        public void CreateBooking(Booking booking);
        public Task<bool> IsSaveChanges();
    }
}
