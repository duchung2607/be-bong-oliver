namespace BongOliver.DTOs.Product
{
    public class ProductDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public double price { get; set; }
        public int productTypeId { get; set; }
    }
}
