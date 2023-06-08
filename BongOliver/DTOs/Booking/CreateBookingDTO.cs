using BongOliver.DTOs.BookingDetail;
using BongOliver.DTOs.Service;

namespace BongOliver.DTOs.Booking
{
    public class CreateBookingDTO
    {
        public DateTime time { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public int userId { get; set; }
        public int stylistId { get; set; }
        public List<int> serviceIds { get; set; }
    }
}
