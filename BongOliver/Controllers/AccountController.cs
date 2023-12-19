using BongOliver.DTOs.Order;
using BongOliver.DTOs.User;
using BongOliver.Services.AuthService;
using BongOliver.Services.EmailService;
using BongOliver.Services.PaymentService;
using BongOliver.Services.UserService;
using BongOliver.Services.VnPayService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace BongOliver.Controllers
{
    [Route("api/my")]
    [ApiController]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IVnPayService _vnPayService;
        private readonly IAuthService _authService;
        private readonly IPaymentService _paymentService;
        public AccountController(IUserService userService, IEmailService emailService, IVnPayService vnPayService
            , IAuthService authService, IPaymentService paymentService)
        {
            _userService = userService;
            _emailService = emailService;
            _vnPayService = vnPayService;
            _authService = authService;
            _paymentService = paymentService;
        }
        [HttpGet]
        public ActionResult My()
        {
            var userClaimsList = HttpContext.User.Claims.ToList();
            var res = _userService.GetUserByUsername(userClaimsList[0].Value.ToString());
            return StatusCode(res.code, res);
        }
        [HttpGet("verify")]
        public ActionResult Verify()
        {
            var claims = HttpContext.User.Claims.ToList();
            var username = claims[0].Value.ToString();
            var res = _userService.VerifyEmail(username);

            return StatusCode(res.code, res);
        }
        
        [HttpPost("change")]
        public ActionResult ChangePassword(ChangePassDTO changePassDTO)
        {
            var claims = HttpContext.User.Claims.ToList();
            var username = claims[0].Value.ToString();

            var res = _userService.ChangePass(changePassDTO, username);

            return StatusCode(res.code, res);
        }
        [HttpPut("update")]
        public ActionResult UpdateProfile(UpdateUser updateUser)
        {
            var claims = HttpContext.User.Claims.ToList();
            var username = claims[0].Value.ToString();
            var res = _userService.UpdateUser(updateUser, username);
            return StatusCode(res.code, res);
        }
        [HttpPost("payin")]
        public async Task<ActionResult> PayIn(double money)
        {
            var claims = HttpContext.User.Claims.ToList();
            var username = claims[0].Value.ToString();
            var res = await _vnPayService.CreateUrlPayIn(username, money);
            return StatusCode(res.code, res);
        }

        [HttpPost("checkpass")]
        public async Task<ActionResult> CheckPass(string pass)
        {
            var claims = HttpContext.User.Claims.ToList();
            var username = claims[0].Value.ToString();
            var res = _authService.CheckPass(username, pass);
            return StatusCode(res.code, res);
        }

        [HttpPost("paywalet")]
        public async Task<IActionResult> PayWalet(int bookingId)
        {
            var claims = HttpContext.User.Claims.ToList();
            var username = claims[0].Value.ToString();
            var res = await _userService.PayMentWithWalet(username, bookingId);
            return StatusCode(res.code, res);
        }

        //[HttpPut("order")]
        //public ActionResult CreateOrder(CreateOrderDTO createOrderDTO)
        //{
        //    var claims = HttpContext.User.Claims.ToList();
        //    var username = claims[0].Value.ToString();

        //    var res = _userService.UpdateUser(updateUser, username);
        //    return StatusCode(res.code, res);
        //}
    }
}
