using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BongOliver.Models
{
    public class Walet
    {
        [Key]
        public int id { get; set; }
        public double money { get; set; } = 0;
        public DateTime create { get; set; } = DateTime.Now;
        public DateTime update { get; set; } = DateTime.Now;
        //public int userId { get; set; }
        public virtual User? User { get; set; }
    }
}