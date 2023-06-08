using BongOliver.DTOs.User;
using BongOliver.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BongOliver.Controllers
{
    [Route("api/stylist")]
    [ApiController]
    public class StylistController : ControllerBase
    {
        private readonly IUserService _userService;
        public StylistController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("")]
        public ActionResult<IEnumerable<UserDTO>> GetStylists()
        {
            return Ok(_userService.GetUsers());
        }
    }
}
