using BongOliver.DTOs.Response;

namespace BongOliver.Services.StatisticalService
{
    public interface IStatisticalService
    {
        ResponseDTO StatisticalUser();
        ResponseDTO StatisticalRevenue();
        ResponseDTO StatisticalMostOfService();
    }
}
