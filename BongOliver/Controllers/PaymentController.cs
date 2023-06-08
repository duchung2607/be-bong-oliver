using BongOliver.Services.PaymentService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BongOliver.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetPaymentListAsync()
        {
            var resData = await _paymentService.GetPaymentListAsync();
            return StatusCode(resData.code, resData);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentListAsync(int id)
        {
            var resData = await _paymentService.GetPaymentById(id);
            return StatusCode(resData.code, resData);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var resData = await _paymentService.DeletePayment(id);
            return StatusCode(resData.code, resData);
        }
    }
}
