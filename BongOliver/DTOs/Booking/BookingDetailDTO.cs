namespace BongOliver.DTOs.Booking
{
    public class BookingDetailDTO
    {
        public int id { get; set; }
        public string description { get; set; }
        public string status { get; set; } = "wait";
        public DateTime time { get; set; } = DateTime.Now;
        public double price { get; set; }
        //public DateTime create { get; set; } = DateTime.Now;
        //public DateTime update { get; set; } = DateTime.Now;
        public  string usernameUser { get; set; }
        public  string usernameStylist { get; set; }
        public List<ServiceBookingDTO> ServiceBookingDTOs { get; set; }
    }
}
