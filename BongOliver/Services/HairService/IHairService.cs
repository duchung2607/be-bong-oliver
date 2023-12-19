using BongOliver.DTOs.Hair;
using BongOliver.DTOs.Response;
using BongOliver.Models;

namespace BongOliver.Services.HairService
{
    public interface IHairService
    {
        ResponseDTO CreateHairStyle(HairStyleDTO hairStyleDTO);
        ResponseDTO UpdateHairStyle(HairStyleDTO hairStyleDTO, int id);
        ResponseDTO GetAllHairStyles(int? page = 1, int? pageSize = 10, string? key = "");
        //List<HairStyle> GetHairStylesByUser(int id);
        ResponseDTO GetHairStyleById(int id);
        ResponseDTO DeleteHairStyle(int id);
    }
}
