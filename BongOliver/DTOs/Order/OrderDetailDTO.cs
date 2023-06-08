using BongOliver.DTOs.Product;
using BongOliver.DTOs.User;

namespace BongOliver.DTOs.Order
{
    public class OrderDetailDTO
    {
        public int id { get; set; }
        public string status { get; set; } = "Pending";
        public virtual UserDTO userDTO { get; set; }
        public virtual List<ProductDTO> productDTOs { get; set; }
    }
}
