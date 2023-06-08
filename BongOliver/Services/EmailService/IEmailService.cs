using BongOliver.DTOs.Response;

namespace BongOliver.Services.EmailService
{
    public interface IEmailService
    {
        ResponseDTO SendEmail(string to, string subject, string body);
        ResponseDTO VerifyEmail(string email, string token);
    }
}
