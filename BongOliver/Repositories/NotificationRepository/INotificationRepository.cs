using BongOliver.Models;

namespace BongOliver.Repositories.NotificationRepository
{
    public interface INotificationRepository
    {
        void CreateNotification(Notification notification);
        void UpdateNotification(Notification notification);
        List<Notification> GetAllNotifications();
        List<Notification> GetNotificationsByUser(int id);
        Notification GetNotificationById(int id);
        void DeleteNotification(Notification notification);
        bool IsSaveChanges();

    }
}
