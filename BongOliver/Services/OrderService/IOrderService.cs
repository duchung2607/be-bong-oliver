using BongOliver.DTOs.Order;
using BongOliver.DTOs.Response;

namespace BongOliver.Services.OrderService
{
    public interface IOrderService
    {
        ResponseDTO GetOrders(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id");
        ResponseDTO GetOrderById(int id);
        ResponseDTO CreateOrder(CreateOrderDTO createOrderDTO);
        ResponseDTO DeleteOrder(int id);
    }
}
