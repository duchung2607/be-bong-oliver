using BongOliver.DTOs.Role;
using BongOliver.Services.RoleService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BongOliver.Controllers
{
    [Route("api/roles")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet("")]
        public ActionResult<List<RoleDTO>> GetRoles()
        {
            return Ok(_roleService.GetRoles());
        }
        [HttpPost("")]
        public ActionResult Create(RoleDTO role)
        {
            if (role == null)
            {
                return BadRequest("Role null");
            }
            return Ok(_roleService.CreateRole(role));
        }
    }
}
