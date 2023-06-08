using BongOliver.Models;

namespace BongOliver.Repositories.OrderRepository
{
    public interface IOrderRepository
    {
        List<Order> GetOrders(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id");
        Order GetOrderById(int id);
        void DeleteOrder(Order order);
        void CreateOrder(Order order);
        void UpdateOrder(Order order);
        bool IsSaveChanges();
    }
}
