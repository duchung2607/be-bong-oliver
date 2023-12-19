using BongOliver.DTOs.Hair;
using BongOliver.DTOs.Notification;
using BongOliver.DTOs.Service;
using BongOliver.Models;
using BongOliver.Services.HairService;
using BongOliver.Services.NotificationService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BongOliver.Controllers
{
    [Route("api/hair")]
    [ApiController]
    public class HairController : ControllerBase
    {
        private readonly IHairService _hairService;
        public HairController(IHairService hairService)
        {
            _hairService = hairService;
        }
        [HttpGet]
        public ActionResult GetAllHairs(int? page = 1, int? pageSize = 10, string? key = "")
        {
            var res = _hairService.GetAllHairStyles(page, pageSize, key);
            return StatusCode(res.code, res);
        }

        [HttpGet("{id}")]
        public ActionResult GetHairById(int id)
        {
            var res = _hairService.GetHairStyleById(id);
            return StatusCode(res.code, res);
        }
        //[HttpGet("user/{id}")]
        //public ActionResult GetHairByUser(int id)
        //{
        //    var res = _hairService.GetNotificationsByUser(id);
        //    return StatusCode(res.code, res);
        //}
        [HttpPut("{id}")]
        public ActionResult UpdateHairStyle(HairStyleDTO hairStyleDTO, int id)
        {
            var res = _hairService.UpdateHairStyle(hairStyleDTO, id);
            return StatusCode(res.code, res);
        }
        [HttpPost]
        public ActionResult CreateHair(HairStyleDTO hairStyleDTO)
        {
            var res = _hairService.CreateHairStyle(hairStyleDTO);
            return StatusCode(res.code, res);
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteHair(int id)
        {
            var res = _hairService.DeleteHairStyle(id);
            return StatusCode(res.code, res);
        }
    }
}
