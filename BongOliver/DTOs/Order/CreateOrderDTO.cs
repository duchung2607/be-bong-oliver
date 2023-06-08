using BongOliver.Models;

namespace BongOliver.DTOs.Order
{
    public class CreateOrderDTO
    {
        public string status { get; set; } = "Pending";
        public int userId { get; set; }
        public virtual List<int> productIds { get; set; }
    }
}
