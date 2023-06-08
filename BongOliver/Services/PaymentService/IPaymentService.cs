using BongOliver.DTOs.Response;
using BongOliver.Models;

namespace BongOliver.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<ResponseDTO> GetPaymentListAsync();
        Task<ResponseDTO> CreatePaymentAsync(Payment payment);
        Task<ResponseDTO> DeletePayment(int id);
        Task<ResponseDTO> GetPaymentById(int id);
    }
}
