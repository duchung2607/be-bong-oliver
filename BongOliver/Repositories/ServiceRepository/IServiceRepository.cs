using BongOliver.Models;

namespace BongOliver.Repositories.ServiceRepository
{
    public interface IServiceRepository
    {
        List<Service> GetServices(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id");
        Service GetServiceById(int id);
        List<ServiceType> GetTypes();
        void DeleteService(Service service);
        void UpdateService(Service service);
        void CreateService(Service service);
        int GetTotal();
        bool IsSaveChanges();
    }
}
