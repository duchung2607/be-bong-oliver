using BongOliver.DTOs.Rate;
using BongOliver.DTOs.Response;
using BongOliver.Models;

namespace BongOliver.Services.RateService
{
    public interface IRateService
    {
        ResponseDTO GetRates(int? page = 1, int? pageSize = 10);
        ResponseDTO GetRateById(int id);
        ResponseDTO GetRateByService(int id);
        ResponseDTO CreateRate(CreateRateDTO createRateDTO);
        ResponseDTO UpdateRate(RateDTO rateDTO);
        ResponseDTO DeleteRate(int id);
    }
}
