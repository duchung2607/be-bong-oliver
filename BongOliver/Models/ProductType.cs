using System.ComponentModel.DataAnnotations;

namespace BongOliver.Models
{
    public class ProductType
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public virtual List<Product> Products { get; set;}
    }
}
