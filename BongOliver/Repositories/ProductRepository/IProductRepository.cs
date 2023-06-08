using BongOliver.Models;

namespace BongOliver.Repositories.ProductRepository
{
    public interface IProductRepository
    {
        void CreateProduct(Product product);
        List<Product> GetProducts(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id");
        Product GetProductById(int id);
        List<ProductType> GetTypes();
        int GetTotal();
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        bool IsSaveChanges();
    }
}
