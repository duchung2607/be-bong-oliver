using System.ComponentModel.DataAnnotations;

namespace BongOliver.Models
{
    public class Role
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public DateTime create { get; set; } = DateTime.Now;
        public DateTime update { get; set; } = DateTime.Now;
        public List<User> users { get; set; }
        public Role() { }
        public Role(int id, string name)
        {
            id = id;
            name = name;
        }
        public Role(string name)
        {
            this.name = name;
        }
    }
}
