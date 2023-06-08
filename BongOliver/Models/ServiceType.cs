using System.ComponentModel.DataAnnotations;

namespace BongOliver.Models
{
    public class ServiceType
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public virtual List<Service> Services { get; set; }
    }
}
