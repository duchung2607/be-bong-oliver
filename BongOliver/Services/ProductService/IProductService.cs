using BongOliver.DTOs.Product;
using BongOliver.DTOs.Response;
using BongOliver.Models;

namespace BongOliver.Services.ProductService
{
    public interface IProductService
    {
        ResponseDTO CreateProduct(ProductDTO productDTO);
        ResponseDTO UpdateProduct(ProductDTO productDTO,int id);
        ResponseDTO DeleteProduct(int id);
        ResponseDTO GetProducts(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id");
        ResponseDTO GetProductById(int id);
        ResponseDTO GetTypes();
        ResponseDTO UpdateImageProduct(string image, int id);
    }
}
