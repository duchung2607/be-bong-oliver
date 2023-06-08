using BongOliver.Services.VnPayService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BongOliver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VnPayController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;
        public VnPayController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUrlPayment(int bookingId, double total)
        {
            var resData = await _vnPayService.CreateUrlPayment(bookingId, total);
            return StatusCode(resData.code, resData);
        }
        [HttpGet()]
        public async Task<IActionResult> ReturnPayment()
        {
            var vnpayData = Request.Query;
            var resData = await _vnPayService.ReturnPayment(vnpayData);
            return StatusCode(resData.code, resData);
        }

        [HttpPost("payin")]
        public async Task<IActionResult> CreateUrlPayIn(string username, double money)
        {
            var resData = await _vnPayService.CreateUrlPayIn(username,money);
            return StatusCode(resData.code, resData);
        }
        [HttpGet("payin")]
        public async Task<IActionResult> ReturnPayIn()
        {
            var vnpayData = Request.Query;
            var resData = await _vnPayService.ReturnPayIn(vnpayData);
            return StatusCode(resData.code, resData);
        }
    }
}
