using BongOliver.Services.StatisticalService;
using BongOliver.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BongOliver.Controllers
{
    [Route("api/statistical")]
    [ApiController]
    public class StatisticalController : ControllerBase
    {
        private readonly IStatisticalService _statisticalService;
        public StatisticalController(IStatisticalService statisticalService)
        {
            _statisticalService = statisticalService;
        }
        [HttpGet("user")]
        public ActionResult StatisticalUser()
        {
            var res = _statisticalService.StatisticalUser();
            return StatusCode(res.code, res);
        }
        [HttpGet("revenue")]
        public ActionResult StatisticalRevenue()
        {
            var res = _statisticalService.StatisticalRevenue();
            return StatusCode(res.code, res);
        }
    }
}
