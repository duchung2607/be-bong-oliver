using BongOliver.Models;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace BongOliver.Models.Seed
{
    public class Seed 
    {
        public static void SeedUsers(DataContext context)
        {     
            if (context.Roles.Any()) return;
            List<Role> roles = new List<Role>()
            {
                new Role() {name="admin"},
                new Role() {name="stylist"},
                new Role() {name="user"},
                new Role() {name="staff"},
                new Role() {name="guest"}
            };
            context.Roles.AddRange(roles);
            context.SaveChanges();
        }
    }
}