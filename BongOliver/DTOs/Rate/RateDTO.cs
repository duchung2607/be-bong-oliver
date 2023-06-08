namespace BongOliver.DTOs.Rate
{
    public class RateDTO
    {
        public int id { get; set; }
        public int rate { get; set; }
        public string comment { get; set; }
        public DateTime create { get; set; } = DateTime.Now;
        public DateTime update { get; set; } = DateTime.Now;
        public int userId { get; set; }
        public int serviceId { get; set; }
    }
}
