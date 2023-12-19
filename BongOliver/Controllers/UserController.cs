using BongOliver.DTOs.Response;
using BongOliver.DTOs.User;
using BongOliver.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BongOliver.Controllers
{
    [Route("api/users")]
    [ApiController]
    //[Authorize(Roles = "admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("")]
        public ActionResult<IEnumerable<UserDTO>> GetUsers(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id")
        {
            var res = _userService.GetUsers(page, pageSize, key, sortBy);
            return StatusCode(res.code, res);
        }
        [HttpGet("{username}")]
        public ActionResult<UserDTO> GetUser(string username)
        {
            return Ok(_userService.GetUserByUsername(username));
        }
        [HttpDelete("{username}")]
        public ActionResult DeleteUser(string username)
        {
            var res = _userService.DeleteUser(username);
            return StatusCode(res.code, res);
        }
        [Route("stylist")]
        [HttpGet]
        public ActionResult GetStylist()
        {
            var res = _userService.GetStylist();
            return StatusCode(res.code, res);
        }
        [HttpPost("create")]
        public ActionResult CreateUser([FromBody] CreateUserDTO createUser)
        {
            var res = _userService.CreateUser(createUser);
            return StatusCode(res.code, res);
        }
        [HttpPut("{username}")]
        public ActionResult UpdateUser([FromBody] UpdateUser updateUser, string username)
        {
            var res = _userService.UpdateUser(updateUser, username);
            return StatusCode(res.code, res);
        }
        [HttpPut("walet/{username}")]
        public ActionResult UpdateWaletUser(double money, string username)
        {
            var res = _userService.UpdateWaletUser(username, money);
            return StatusCode(res.code, res);
        }
        [HttpPost("verify")]
        public ActionResult VerifyEmail(string username)
        {
            try
            {
                var res = _userService.VerifyEmail(username);
                res.message = "Please check your email to verify this account!";
                return StatusCode(res.code, res);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPost("ids")]
        public ActionResult GetUserByIds(List<int> ids)
        {
            var res = _userService.GetUserByIds(ids);
            return StatusCode(res.code, res);
        }
    }
}
