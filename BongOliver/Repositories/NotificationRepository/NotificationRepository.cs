using BongOliver.Models;
using Microsoft.EntityFrameworkCore;

namespace BongOliver.Repositories.NotificationRepository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly DataContext _context;

        public NotificationRepository(DataContext context)
        {
            _context = context;
        }
        public void CreateNotification(Notification notification)
        {
            _context.Notifications.Add(notification);
        }

        public void DeleteNotification(Notification notification)
        {
            _context.Notifications.Remove(notification);
        }

        public List<Notification> GetAllNotifications()
        {
            return _context.Notifications.ToList();
        }

        public Notification GetNotificationById(int id)
        {
            return _context.Notifications.Include(n => n.User).FirstOrDefault(n => n.Id == id);
        }

        public List<Notification> GetNotificationsByUser(int id)
        {
            return _context.Notifications.Include(n => n.User).Where(n => n.User.id == id).ToList();
        }

        public bool IsSaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void UpdateNotification(Notification notification)
        {
            _context.Notifications.Update(notification);
        }
    }
}
