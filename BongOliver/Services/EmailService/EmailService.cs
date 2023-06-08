using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using static Org.BouncyCastle.Math.EC.ECCurve;
using BongOliver.Repositories.UserRepository;
using BongOliver.Services.TokenService;
using BongOliver.DTOs.Response;

namespace BongOliver.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _config;
        public EmailService(IUserRepository userRepository, ITokenService tokenService, IConfiguration config)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _config = config;
        }
        public ResponseDTO SendEmail(string to, string subject, string body)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_config.GetSection("Email:From").Value));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;
                email.Body = new TextPart(TextFormat.Text) { Text = body };

                using var smtp = new SmtpClient();

                smtp.Connect(_config.GetSection("Email:Host").Value, int.Parse(_config.GetSection("Email:Port").Value), SecureSocketOptions.StartTls);
                smtp.Authenticate(_config.GetSection("Email:From").Value, _config.GetSection("Email:Password").Value);
                smtp.Send(email);
                smtp.Disconnect(true);
                return new ResponseDTO()
                {
                    code = 200,
                    message = "Success! Please check your email",
                    data = null
                };
            }
            catch (Exception e)
            {
                return new ResponseDTO()
                {
                    code = 400,
                    message = "Faile. " + e.Message,
                    data = null
                };
            }
        }

        public ResponseDTO VerifyEmail(string email, string token)
        {
            try
            {
                var user = _userRepository.GetUserByEmail(email);
                if (user != null && user.phone == token)
                    return new ResponseDTO()
                    {
                        code = 200,
                        message = "Success! Your email is verify!",
                        data = null
                    };
                else
                    return new ResponseDTO()
                    {
                        code = 400,
                        message = "Faile",
                        data = null
                    };
            }
            catch (Exception e)
            {
                return new ResponseDTO()
                {
                    code = 400,
                    message = "Faile. " + e.Message,
                    data = null
                };
            }
        }
    }
}
