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
        public ActionResult GetRates()
        {
            var res = _rateService.GetRates();
            return StatusCode(res.code, res);
        }
        [HttpGet("{id}")]
        public ActionResult GetRateById(int id)
        {
            var res = _rateService.GetRateById(id);
            return StatusCode(res.code, res);
        }
        [HttpPost]
        [Authorize]
        public ActionResult CreateRate(CreateRateDTO createRateDTO)
        {
            var res = _rateService.CreateRate(createRateDTO);
            return StatusCode(res.code, res);
        }
    }
}
