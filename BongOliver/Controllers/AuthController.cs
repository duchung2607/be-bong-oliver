using BongOliver.DTOs.User;
using BongOliver.Services.AuthService;
using BongOliver.Services.UserService;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace BongOliver.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        public AuthController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }
        [HttpPost("register")]
        public ActionResult Register([FromBody] RegisterUserDTO registerUserDto)
        {
            try
            {
                var res = _authService.Register(registerUserDto);
                return StatusCode(res.code, res);
            }
            catch (Exception e)
            {
                return BadRequest("p -- " + e.Message);
            };
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] AuthUserDTO authUserDto)
        {
            try
            {
                var res = _authService.Login(authUserDto);
                return StatusCode(res.code,res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            };
        }
        [HttpPost("verify")]
        public ActionResult VerifyEmail(string email, string token)
        {
            try
            {
                var res = _authService.VerifyEmail(email, token);
                return StatusCode(res.code, res);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }
        [HttpGet("forgot")]
        public ActionResult ForgotPassword(string email)
        { 
            var res = _authService.ForgotPassword(email);

            return StatusCode(res.code, res);
        }
    }
}
