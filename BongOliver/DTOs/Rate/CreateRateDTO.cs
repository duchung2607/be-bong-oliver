namespace BongOliver.DTOs.Rate
{
    public class CreateRateDTO
    {
        public int rate { get; set; }
        public string comment { get; set; }
        public int userId { get; set; }
        public int serviceId { get; set; }
    }
}
