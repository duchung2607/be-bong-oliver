using BongOliver.DTOs.Order;
using BongOliver.Services.OrderService;
using BongOliver.Services.ProductService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BongOliver.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet("{id}")]
        public ActionResult GetOrderById(int id)
        {
            var res = _orderService.GetOrderById(id);
            return StatusCode(res.code, res);
        }
        [HttpGet]
        public ActionResult GetOrders(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id")
        {
            var res = _orderService.GetOrders(page,pageSize,key,sortBy);
            return StatusCode(res.code, res);
        }
        [HttpPost]
        public ActionResult CreateOrder(CreateOrderDTO createOrderDTO)
        {
            var res = _orderService.CreateOrder(createOrderDTO);
            return StatusCode(res.code, res);
        }
        [HttpDelete]
        public ActionResult DeleteOrder(int id)
        {
            var res = _orderService.DeleteOrder(id);
            return StatusCode(res.code, res);
        }
    }
}
