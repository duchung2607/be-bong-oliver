using System.ComponentModel.DataAnnotations;

namespace BongOliver.Models
{
    public class Order
    {
        [Key]
        public int id { get; set; }
        public string status { get; set; } = "Pending";
        public DateTime create { get; set; } = DateTime.Now;
        public DateTime update { get; set; } = DateTime.Now;
        public int userId { get; set; }
        public virtual User User { get; set; }
        public virtual List<Product> Products { get; set; }
        //public virtual List<OrderItem> OrderItems { get; set; }
    }
}
