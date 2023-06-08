using System.ComponentModel.DataAnnotations;

namespace BongOliver.Models
{
    public class Service
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public string image { get; set; }
        public int serviceTypeId { get; set; }
        public virtual ServiceType ServiceType { get; set; }
        public virtual List<Booking> Bookings { get; set; }
        public virtual List<Rate> Rates { get; set; }
    }
}
