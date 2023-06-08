using BongOliver.DTOs.Service;
using BongOliver.Services.RoleService;
using BongOliver.Services.ServiceService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BongOliver.Controllers
{
    [Route("api/service")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }
        [HttpGet]
        public ActionResult GetServices(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id")
        {
            var res = _serviceService.GetServices(page,pageSize,key,sortBy);
            return StatusCode(res.code, res);
        }
        [HttpGet("{id}")]
        public ActionResult GetServiceById(int id)
        {
            var res = _serviceService.GetServiceById(id);
            return StatusCode(res.code, res);
        }
        //[HttpPost("create")]
        [HttpPost]
        public ActionResult CreateService(CreateServiceDTO serviceDTO)
        {
            var res = _serviceService.CreateService(serviceDTO);
            return StatusCode(res.code, res);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteService(int id)
        {
            var res = _serviceService.DeleteService(id);
            return StatusCode(res.code, res);
        }
        [HttpPut("{id}")]
        public ActionResult UpdateService(CreateServiceDTO serviceDTO, int id)
        {
            var res = _serviceService.UpdateService(serviceDTO, id);
            return StatusCode(res.code, res);
        }
        [HttpGet("type")]
        public ActionResult GetTypes()
        {
            var res = _serviceService.GetServiceTypes();
            return StatusCode(res.code, res);
        }
    }
}
