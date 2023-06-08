using System.ComponentModel.DataAnnotations;

namespace BongOliver.Models
{
    public class Product
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public double price { get; set; }
        public DateTime create { get; set; } = DateTime.Now;
        public DateTime update { get; set; } = DateTime.Now;
        public int productTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }
        public virtual List<Order> Orders { get; set; }
        //public virtual List<OrderItem> OrderItems { get; set; }
    }
}
