using BongOliver.DTOs.Booking;
using BongOliver.DTOs.Response;
using BongOliver.Models;

namespace BongOliver.Services.BookingService
{
    public interface IBookingService
    {
        public ResponseDTO GetAllBooking(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id");
        public ResponseDTO GetBookingByUser(string username, int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id");
        public ResponseDTO GetBookingByStylist(string username, int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id");
        public ResponseDTO GetBookingById(int id);
        public ResponseDTO GetSchedules(int stylistId, DateTime date);
        public ResponseDTO GetCalendar();
        public Task<ResponseDTO> UpdateBooking(UpdateBookingDTO dto, int id);
        public Task<ResponseDTO> DeleteBooking(int id);
        public Task<ResponseDTO> CreateBooking(CreateBookingDTO createBookingDTO);
        public Task<bool> IsSaveChanges();
    }
}
