using BongOliver.DTOs.Response;
using BongOliver.DTOs.Service;

namespace BongOliver.Services.ServiceService
{
    public interface IServiceService
    {
        ResponseDTO GetServices(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id");
        ResponseDTO GetServiceById(int id);
        ResponseDTO CreateService(CreateServiceDTO serviceDTO);
        ResponseDTO UpdateService(CreateServiceDTO serviceDTO, int id);
        ResponseDTO GetServiceTypes();
        ResponseDTO DeleteService(int id);

    }
}
