using System.ComponentModel.DataAnnotations;

namespace BongOliver.DTOs.User
{
    public class RegisterUserDTO
    {
        [Required]
        [StringLength(32)]
        public string username { get; set; }
        public string? name { get; set; }
        //[Required]
        //[StringLength(10)]
        //public string phone { get; set; }
        public string email { get; set; }
        [Required]
        [StringLength(32)]
        public string password { get; set; }
        [Required]
        [StringLength(32)]
        public string cfpassword { get; set; }
        //[Required]
        //public bool gender { get; set; }
    }
}
