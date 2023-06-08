using System.ComponentModel.DataAnnotations;

namespace BongOliver.Models
{
    public class Booking
    {
        [Key]
        public int id { get; set; }
        public string description { get; set; }
        public string status { get; set; } = "wait";
        public DateTime time { get; set; } = DateTime.Now;
        public DateTime create { get; set; } = DateTime.Now;
        public DateTime update { get; set; } = DateTime.Now;
        public int userId { get; set; }
        public virtual User User { get; set; }
        public int stylistId { get; set; }
        public virtual User Stylist { get; set; }
        public virtual List<Service> Services { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
