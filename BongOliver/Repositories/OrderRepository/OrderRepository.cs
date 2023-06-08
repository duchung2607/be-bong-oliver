using BongOliver.Models;
using Microsoft.EntityFrameworkCore;

namespace BongOliver.Repositories.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;
        public OrderRepository(DataContext dataContext)
        {
            _context = dataContext;
        }
        public void CreateOrder(Order order)
        {
            _context.Orders.Add(order);
        }

        public void DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders.Include(p => p.Products).Include(u=>u.User).FirstOrDefault(o => o.id == id);
        }

        public List<Order> GetOrders(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id")
        {
            var query = _context.Orders.Include(p => p.Products).AsQueryable();

            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(u => u.status.ToLower().Contains(key.ToLower()));
            }

            switch (sortBy)
            {
                case "status":
                    query = query.OrderBy(u => u.status);
                    break;
                default:
                    query = query.OrderBy(u => u.id);
                    break;
            }
            if (page == null || pageSize == null || sortBy == null) { return query.ToList(); }
            else
                return query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();
        }

        public bool IsSaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
