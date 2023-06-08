using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BongOliver.Models
{
    public class Payment
    {
        [Key]
        public int id { get; set; }
        public double total { get; set; }
        public DateTime time { get; set; } = DateTime.Now;
        public string mode { get; set; }
        [ForeignKey("Bookings")]
        public int bookingId { get; set; }
        public virtual Booking Booking { get; set; }
    }
}
