using System.ComponentModel.DataAnnotations;

namespace BongOliver.Models
{
    public class Rate
    {
        [Key]
        public int id { get; set; }
        public int rate { get; set; }
        public string comment { get; set; }
        public DateTime create { get; set; } = DateTime.Now;
        public DateTime update { get; set; } = DateTime.Now;
        public int userId { get; set; }
        public virtual User User { get; set; }
        public int serviceId { get; set; }
        public virtual Service Service { get; set; }
    }
}
