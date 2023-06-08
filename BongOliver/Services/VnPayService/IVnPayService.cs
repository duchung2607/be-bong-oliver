using BongOliver.DTOs.Response;

namespace BongOliver.Services.VnPayService
{
    public interface IVnPayService
    {
        Task<ResponseDTO> CreateUrlPayment(int bookingId, double total);
        Task<ResponseDTO> CreateUrlPayIn(string username, double money);
        Task<ResponseDTO> ReturnPayment(IQueryCollection vnpayData);
        Task<ResponseDTO> ReturnPayIn(IQueryCollection vnpayData);
    }
}
