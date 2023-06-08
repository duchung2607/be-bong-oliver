using BongOliver.DTOs.Role;
using BongOliver.DTOs.Walet;
using System.ComponentModel.DataAnnotations;
using static System.Net.WebRequestMethods;

namespace BongOliver.DTOs.User
{
    public class CreateUserDTO
    {
        [Required]
        [StringLength(32)]
        public string username { get; set; }
        public string? name { get; set; }
        public string avatar { get; set; } = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/59/User-avatar.svg/2048px-User-avatar.svg.png";
        [Required]
        [StringLength(10)]
        public string phone { get; set; }
        public string email { get; set; }
        [Required]
        [StringLength(32)]
        public string password { get; set; }
        [Required]
        public bool gender { get; set; }
        public int roleId { get; set; }
        public WaletDTO WaletDTO { get; set; }
    }
}
