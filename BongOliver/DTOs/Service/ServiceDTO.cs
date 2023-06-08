namespace BongOliver.DTOs.Service
{
    public class ServiceDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public double price { get; set; }
        public double rate { get; set; } = 0;
        public ServiceTypeDTO serviceTypeDTO { get; set; }
    }
}
