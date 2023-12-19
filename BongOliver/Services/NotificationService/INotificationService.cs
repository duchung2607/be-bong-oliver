using BongOliver.DTOs.Notification;
using BongOliver.DTOs.Response;
using BongOliver.Models;

namespace BongOliver.Services.NotificationService
{
    public interface INotificationService
    {
        ResponseDTO CreateNotification(NotificationDTO notificationDTO);
        ResponseDTO UpdateNotification(NotificationDTO notificationDTO);
        ResponseDTO GetAllNotifications();
        ResponseDTO GetNotificationsByUser(int id);
        ResponseDTO GetNotificationById(int id);
        ResponseDTO DeleteNotification(int id);
    }
}
