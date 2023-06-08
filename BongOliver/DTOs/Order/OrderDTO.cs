using BongOliver.DTOs.Product;
using BongOliver.Models;

namespace BongOliver.DTOs.Order
{
    public class OrderDTO
    {
        public int id { get; set; }
        public string status { get; set; } = "Pending";
        public double price { get; set; }
        public DateTime create { get; set; }
    }
}
