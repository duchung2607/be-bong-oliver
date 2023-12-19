using System.ComponentModel.DataAnnotations;

namespace BongOliver.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public string Type { get; set; } = "Text";
        public DateTime Time { get; set; } = DateTime.Now;
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public virtual User Sender { get; set; }
        public virtual User Receiver { get; set; }
    }
}
