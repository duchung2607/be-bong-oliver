using BongOliver.DTOs.Rate;
using BongOliver.Services.ItemService;
using BongOliver.Services.RateService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BongOliver.Controllers
{
    [Route("api/rate")]
    [ApiController]
    public class RateController : ControllerBase
    {
        private readonly IRateService _rateService;
        public RateController(IRateService rateService)
        {
            _rateService = rateService;
        }

        [HttpGet]
        public ActionResult GetRates(int? page = 1, int? pageSize = 10)
        {
            var res = _rateService.GetRates(page, pageSize);
            return StatusCode(res.code, res);
        }
        [HttpGet("{id}")]
        public ActionResult GetRateById(int id)
        {
            var res = _rateService.GetRateById(id);
            return StatusCode(res.code, res);
        }
        [HttpGet("service/{id}")]
        public ActionResult GetRateByService(int id)
        {
            var res = _rateService.GetRateByService(id);
            return StatusCode(res.code, res);
        }
        [HttpPost]
        [Authorize]
        public ActionResult CreateRate(CreateRateDTO createRateDTO)
        {
            var res = _rateService.CreateRate(createRateDTO);
            return StatusCode(res.code, res);
        }
        [HttpDelete("{id}")]
        //[Authorize]
        public ActionResult DeleteRate(int id)
        {
            var res = _rateService.DeleteRate(id);
            return StatusCode(res.code, res);
        }
    }
}
