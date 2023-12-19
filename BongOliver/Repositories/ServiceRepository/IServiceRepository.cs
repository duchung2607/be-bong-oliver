using BongOliver.Models;

namespace BongOliver.Repositories.ServiceRepository
{
    public interface IServiceRepository
    {
        List<Service> GetServices(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id");
        List<Service> GetMostOfService(int? size = 5);
        Service GetServiceById(int id);
        List<ServiceType> GetTypes();
        ServiceType GetTypeById(int id);
        void DeleteTypes(ServiceType serviceType);
        void CreateTypes(ServiceType serviceType);
        void UpdateTypes(ServiceType serviceType);
        void DeleteService(Service service);
        void UpdateService(Service service);
        void CreateService(Service service);
        int GetTotal();
        bool IsSaveChanges();
    }
}
