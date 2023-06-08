using AutoMapper;
using BongOliver.DTOs.Order;
using BongOliver.DTOs.Product;
using BongOliver.DTOs.Response;
using BongOliver.DTOs.User;
using BongOliver.Models;
using BongOliver.Repositories.OrderRepository;
using BongOliver.Repositories.ProductRepository;
using BongOliver.Repositories.UserRepository;
using BongOliver.Services.EmailService;

namespace BongOliver.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        public OrderService(IMapper mapper, IOrderRepository orderRepository, IProductRepository productRepository, IUserRepository userRepository
            , IEmailService emailService)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _emailService = emailService;
        }
        public ResponseDTO CreateOrder(CreateOrderDTO createOrderDTO)
        {
            var order = new Order();
            order.status = createOrderDTO.status;

            var user = _userRepository.GetUserById(createOrderDTO.userId);
            if (user == null) return new ResponseDTO() { code = 400, message = "User is is not valid" };
            //if (user.isVerify) return new ResponseDTO() { code = 400, message = "Your email must be verify" };

            order.userId = createOrderDTO.userId;
            order.Products = new List<Product>();

            foreach(var id in createOrderDTO.productIds)
            {
                var product = _productRepository.GetProductById(id);
                if(product != null)
                order.Products.Add(product);
            }

            _orderRepository.CreateOrder(order);
            if (_orderRepository.IsSaveChanges()) return new ResponseDTO() { };
            else return new ResponseDTO()
            {
                code = 400,
                message = "faile"
            };
        }

        public ResponseDTO DeleteOrder(int id)
        {
            var order = _orderRepository.GetOrderById(id);
            if (order == null) return new ResponseDTO() { code = 400, message = "Id is not valid" };

            _orderRepository.DeleteOrder(order);
            if (_orderRepository.IsSaveChanges()) return new ResponseDTO() { };
            else return new ResponseDTO()
            {
                code = 400,
                message = "faile"
            };
        }

        public ResponseDTO GetOrderById(int id)
        {
            var order = _orderRepository.GetOrderById(id);
            if (order == null) return new ResponseDTO() { code = 400, message = "Id is not valid" };

            var orderDTO = _mapper.Map<OrderDetailDTO>(order);
            orderDTO.productDTOs = new List<ProductDTO>();

            foreach(var product in order.Products)
            {
                var productDTO = _mapper.Map<ProductDTO>(product);
                orderDTO.productDTOs.Add(productDTO);
            }

            orderDTO.userDTO = _mapper.Map<UserDTO>(order.User);

            return new ResponseDTO
            {
                data = orderDTO
            };
        }

        public ResponseDTO GetOrders(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id")
        {
            var orders = _orderRepository.GetOrders(page, pageSize, key, sortBy);

            List<OrderDTO> orderDTOs = new List<OrderDTO>();
            if (orders == null) return new ResponseDTO() { };
            foreach (var order in orders)
            {
                double price = 0;
                var orderDTO = _mapper.Map<OrderDTO>(order);
                foreach(var product in order.Products)
                {
                    price += product.price;
                }
                orderDTO.price = price;
                orderDTOs.Add(orderDTO);
            }

            return new ResponseDTO() { data = orderDTOs };
        }
    }
}
