using BongOliver.DTOs.Role;
using BongOliver.DTOs.Walet;
using BongOliver.Models;
using System.ComponentModel.DataAnnotations;

namespace BongOliver.DTOs.User
{
    public class UserDTO
    {
        public int id { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string? email { get; set; }
        public string phone { get; set; }
        public string? avatar { get; set; }
        public bool gender { get; set; }
        public double rank { get; set; } = 0;
        public DateTime create { get; set; } = DateTime.Now;
        public DateTime update { get; set; } = DateTime.Now;
        public bool isDelete { get; set; }
        public bool isVerify { get; set; }
        public WaletDTO walet { get; set; }
        public RoleDTO role { get; set; }
    }
}
