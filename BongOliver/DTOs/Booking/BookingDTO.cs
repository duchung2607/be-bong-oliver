namespace BongOliver.DTOs.Booking
{
    public class BookingDTO
    {
        public int id { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public string status { get; set; }
        public DateTime time { get; set; }
        public DateTime create { get;set; }
    }
}
