using System.ComponentModel.DataAnnotations;

namespace BongOliver.Models
{
    public class HairStyle
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; } = "";
        public string image { get; set; } = "";
        public bool type { get; set; } = true;
        public string sortDes { get; set; } = "";
        public string description { get; set; } = "";
        public virtual List<User> Users { get; set; }
    }
}
