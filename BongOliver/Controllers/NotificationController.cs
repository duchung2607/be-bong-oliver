using BongOliver.DTOs.Notification;
using BongOliver.Models;
using BongOliver.Services.NotificationService;
using BongOliver.Services.StatisticalService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BongOliver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService) {
            _notificationService = notificationService;
        }
        [HttpGet]
        public ActionResult GetAllNotifications()
        {
            var res = _notificationService.GetAllNotifications();
            return StatusCode(res.code, res);
        }
        [HttpGet("{id}")]
        public ActionResult GetNotificationById(int id)
        {
            var res = _notificationService.GetNotificationById(id);
            return StatusCode(res.code, res);
        }
        [HttpGet("user/{id}")]
        public ActionResult GetNotificationByUser(int id)
        {
            var res = _notificationService.GetNotificationsByUser(id);
            return StatusCode(res.code, res);
        }
        [HttpPost]
        public ActionResult CreateNotification(NotificationDTO notification)
        {
            var res = _notificationService.CreateNotification(notification);
            return StatusCode(res.code, res);
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteNotification(int id)
        {
            var res = _notificationService.DeleteNotification(id);
            return StatusCode(res.code, res);
        }
    }
}
