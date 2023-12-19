using System.ComponentModel.DataAnnotations;

namespace BongOliver.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
