using AutoMapper;
using BongOliver.DTOs.Product;
using BongOliver.DTOs.Response;
using BongOliver.Models;
using BongOliver.Repositories.ProductRepository;
using BongOliver.Repositories.UserRepository;
using BongOliver.Services.EmailService;

namespace BongOliver.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        public ProductService(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }
        public ResponseDTO CreateProduct(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            _productRepository.CreateProduct(product);
            if (_productRepository.IsSaveChanges()) return new ResponseDTO();
            else return new ResponseDTO()
            {
                code = 400,
                message = "Faile"
            };
        }

        public ResponseDTO DeleteProduct(int id)
        {
            var product = _productRepository.GetProductById(id);
            if(product == null) return new ResponseDTO() { code = 400, message = "Is is not valid"};

            _productRepository.DeleteProduct(product);
            if (_productRepository.IsSaveChanges()) return new ResponseDTO();
            else return new ResponseDTO()
            {
                code = 400,
                message = "Faile"
            };
        }

        public ResponseDTO GetProductById(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null) return new ResponseDTO() { code = 400, message = "Is is not valid" };

            return new ResponseDTO()
            {
                data = _mapper.Map<ProductDTO>(product)
            };
        }

        public ResponseDTO GetProducts(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id")
        {
            List<ProductDTO> productDTOs = new List<ProductDTO>();
            var products = _productRepository.GetProducts(page, pageSize, key, sortBy);
            if (products == null) return new ResponseDTO() { };

            foreach(var product in products)
            {
                productDTOs.Add(_mapper.Map<ProductDTO>(product));
            }

            return new ResponseDTO()
            {
                data = productDTOs,
                total = _productRepository.GetTotal()
            };
        }

        public ResponseDTO UpdateProduct(ProductDTO productDTO, int id)
        {
            var product = _productRepository.GetProductById(id);
            product.name = productDTO.name;
            product.price = productDTO.price;
            product.description = productDTO.description;
            product.image = productDTO.image;

            _productRepository.UpdateProduct(product);
            if (_productRepository.IsSaveChanges()) return new ResponseDTO();
            else return new ResponseDTO()
            {
                code = 400,
                message = "Faile"
            };
        }
        public ResponseDTO GetTypes()
        {
            var types = _productRepository.GetTypes();
            return new ResponseDTO() { data = types };
        }

        public ResponseDTO UpdateImageProduct(string image, int id)
        {
            var product = _productRepository.GetProductById(id);
            product.image = image;

            _productRepository.UpdateProduct(product);
            if (_productRepository.IsSaveChanges()) return new ResponseDTO();
            else return new ResponseDTO()
            {
                code = 400,
                message = "Faile"
            };
        }
    }
}
