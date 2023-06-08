using System.ComponentModel.DataAnnotations;

namespace BongOliver.DTOs.User
{
    public class AuthUserDTO
    {
        [Required]
        [MaxLength(256)]
        public String username { get; set; }
        [Required]
        [MaxLength(256)]
        public String password { get; set; }
    }
    public class UserTokenDTO
    {
        public String username { get; set; }
        public String token { get; set; }
    }
}
