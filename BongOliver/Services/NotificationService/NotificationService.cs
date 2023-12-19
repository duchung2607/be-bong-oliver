using AutoMapper;
using BongOliver.DTOs.Notification;
using BongOliver.DTOs.Response;
using BongOliver.Models;
using BongOliver.Repositories.NotificationRepository;
using BongOliver.Repositories.UserRepository;

namespace BongOliver.Services.NotificationService
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public NotificationService(INotificationRepository notificationRepository, IMapper mapper, IUserRepository userRepository)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public ResponseDTO CreateNotification(NotificationDTO notificationDTO)
        {
            var user = _userRepository.GetUserById(notificationDTO.UserId);
            if (user == null) return new ResponseDTO()
            {
                code = 400,
                message = "User không tồn tại"
            };
            var notification = _mapper.Map<Notification>(notificationDTO);
            _notificationRepository.CreateNotification(notification);
            if (_notificationRepository.IsSaveChanges()) return new ResponseDTO()
            {
                message = "Success"
            };
            return new ResponseDTO()
            {
                code = 400,
                message = "Faile"
            };
        }

        public ResponseDTO DeleteNotification(int id)
        {
            var notification = _notificationRepository.GetNotificationById(id);
            if (notification == null) return new ResponseDTO()
            {
                code = 400,
                message = "Thông báo không tồn tại"
            };

            _notificationRepository.DeleteNotification(notification);
            if (_notificationRepository.IsSaveChanges()) return new ResponseDTO()
            {
                message = "Success"
            };
            return new ResponseDTO()
            {
                code = 400,
                message = "Faile"
            };
        }

        public ResponseDTO GetAllNotifications()
        {
            var notifications = _notificationRepository.GetAllNotifications();

            var notificationDTOs = new List<NotificationDTO>();
            foreach(var notification in notifications)
            {
                notificationDTOs.Add(_mapper.Map<NotificationDTO>(notification));
            }

            return new ResponseDTO()
            {
                data = notificationDTOs
            };
        }

        public ResponseDTO GetNotificationById(int id)
        {
            var notification = _notificationRepository.GetNotificationById(id);
            if(notification == null) return new ResponseDTO()
            {
                code = 400,
                message = "Thông báo không tồn tại"
            };

            return new ResponseDTO()
            {
                data = _mapper.Map<NotificationDTO>(notification)
            };
        }

        public ResponseDTO GetNotificationsByUser(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null) return new ResponseDTO()
            {
                code = 400,
                message = "User không tồn tại"
            };
            var notifications = _notificationRepository.GetNotificationsByUser(id);
            var notificationDTOs = new List<NotificationDTO>();
            foreach (var notification in notifications)
            {
                notificationDTOs.Add(_mapper.Map<NotificationDTO>(notification));
            }

            return new ResponseDTO()
            {
                data = notificationDTOs
            };
        }

        public ResponseDTO UpdateNotification(NotificationDTO notificationDTO)
        {
            throw new NotImplementedException();
        }
    }
}
