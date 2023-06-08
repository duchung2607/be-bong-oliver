using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BongOliver.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(100)]
        public string name { get; set; }
        public string? email { get; set; }
        [Required]
        [StringLength(10)]
        public string phone { get; set; }
        public string? avatar { get; set; } = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/59/User-avatar.svg/2048px-User-avatar.svg.png";
        public bool gender { get; set; } = true;
        public DateTime dob { get; set; } = DateTime.Now;
        public double rank { get; set; } = 0;
        [Required]
        [StringLength(32)]
        public string username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool isDelete { get; set; } = false;
        public bool isVerify { get; set; } = false;
        public string token { get; set; }
        public DateTime create { get; set; } = DateTime.Now;
        public DateTime update { get; set; } = DateTime.Now;

        public int roleId { get; set; }
        public Role role { get; set; }
        public int waletId { get; set; }
        public virtual Walet Walet { get; set; }
        public virtual List<Booking> Bookings { get; set; }
        public virtual List<Order> Orders { get; set; }
        public virtual List<Rate> Rates { get; set; }
    }
}
