namespace BongOliver.DTOs.Service
{
    public class CreateServiceDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public double price { get; set; }
        public int serviceTypeId { get; set; }
    }
}
