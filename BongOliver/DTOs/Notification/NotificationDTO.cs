namespace BongOliver.DTOs.Notification
{
    public class NotificationDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        public int UserId { get; set; }
    }
}
