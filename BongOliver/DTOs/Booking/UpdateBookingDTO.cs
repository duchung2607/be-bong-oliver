namespace BongOliver.DTOs.Booking
{
    public class UpdateBookingDTO
    {
        public string description { get; set; }
        public double price { get; set; }
        public string status { get; set; }
        public DateTime time { get; set; }
    }
}
