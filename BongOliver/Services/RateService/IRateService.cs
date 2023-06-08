using BongOliver.DTOs.Rate;
using BongOliver.DTOs.Response;
using BongOliver.Models;

namespace BongOliver.Services.RateService
{
    public interface IRateService
    {
        ResponseDTO GetRates();
        ResponseDTO GetRateById(int id);
        ResponseDTO CreateRate(CreateRateDTO createRateDTO);
        ResponseDTO UpdateRate(RateDTO rateDTO);
        ResponseDTO DeleteRate(int id);
    }
}
