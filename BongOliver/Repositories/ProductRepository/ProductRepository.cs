using BongOliver.Models;
using Microsoft.EntityFrameworkCore;

namespace BongOliver.Repositories.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        public ProductRepository(DataContext dataContext)
        {
            _context = dataContext;
        }
        public void CreateProduct(Product product)
        {
            _context.Products.Add(product);
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
        }

        public Product GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.id == id);
        }

        public List<Product> GetProducts(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id")
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(u => u.name.ToLower().Contains(key.ToLower()));
            }

            switch (sortBy)
            {
                case "name":
                    query = query.OrderBy(u => u.name);
                    break;
                case "price":
                    query = query.OrderBy(u => u.price);
                    break;
                case "price DESC":
                    query = query.OrderByDescending(u => u.price);
                    break;
                default:
                    query = query.OrderBy(u => u.id);
                    break;
            }
            if (page == null || pageSize == null || sortBy == null) { return query.ToList(); }
            else
                return query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();
        }

        public int GetTotal()
        {
            return _context.Products.Count();
        }

        public List<ProductType> GetTypes()
        {
            return _context.ProductTypes.ToList();
        }

        public bool IsSaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void UpdateProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
        }
    }
}
