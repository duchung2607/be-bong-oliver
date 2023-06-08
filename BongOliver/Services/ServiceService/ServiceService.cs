using AutoMapper;
using BongOliver.DTOs.Rate;
using BongOliver.DTOs.Response;
using BongOliver.DTOs.Service;
using BongOliver.Models;
using BongOliver.Repositories.BookingRepository;
using BongOliver.Repositories.ServiceRepository;

namespace BongOliver.Services.ServiceService
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        public ServiceService(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }
        public double GetRateByService(List<Rate> rates)
        {
            double rateAvg = 0;
            if (rates == null) return 0;
            if (rates.Count == 0) return 0;
            foreach (var rate in rates)
            {
                rateAvg += rate.rate;
            }
            return rateAvg/(rates.Count());
        }
        public ResponseDTO CreateService(CreateServiceDTO serviceDTO)
        {
            var service = _mapper.Map<Service>(serviceDTO);
            _serviceRepository.CreateService(service);
            if(_serviceRepository.IsSaveChanges()) return new ResponseDTO();
            return new ResponseDTO() { code = 400, message = "Create Faile"};

        }

        public ResponseDTO DeleteService(int id)
        {
            //var service = _serviceRepository.GetServiceById(id);
            //service
            var service = _serviceRepository.GetServiceById(id);
            if (service == null) return new ResponseDTO() { code = 400, message = "Id is not valid" };
            _serviceRepository.DeleteService(service);
            if (_serviceRepository.IsSaveChanges()) return new ResponseDTO();
            return new ResponseDTO() { code = 400, message = "Delete Faile" };
        }

        public ResponseDTO GetServiceById(int id)
        {
            var service = _serviceRepository.GetServiceById(id);
            var rate = GetRateByService(service.Rates);
            if (service == null) return new ResponseDTO()
            {
                code = 400,
                message = "Id is not valid"
            };

            var serviceDTO = _mapper.Map<ServiceDTO>(service);
            serviceDTO.serviceTypeDTO = _mapper.Map<ServiceTypeDTO>(service.ServiceType);
            serviceDTO.rate = rate;
            //serviceDTO.serviceType = new Object()
            //{
            //    id = service.ServiceType.id,
            //    name = service.ServiceType.name
            //};
            return new ResponseDTO()
            {
                data = serviceDTO
            };
        }

        public ResponseDTO GetServices(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id")
        {
            var serviceDTOs = new List<ServiceDTO>();
            var services = _serviceRepository.GetServices(page, pageSize, key, sortBy);
            foreach(var service in services)
            {
                var rate = GetRateByService(service.Rates);
                var serviceDTO = _mapper.Map<ServiceDTO>(service);
                serviceDTO.rate = rate;
                serviceDTO.serviceTypeDTO = _mapper.Map<ServiceTypeDTO>(service.ServiceType);
                serviceDTOs.Add(serviceDTO);
            }
            return new ResponseDTO()
            {
                data = serviceDTOs,
                total = _serviceRepository.GetTotal()
            };
        }

        public ResponseDTO GetServiceTypes()
        {
            var types = _serviceRepository.GetTypes();
            return new ResponseDTO() { data = types};
        }

        public ResponseDTO UpdateService(CreateServiceDTO serviceDTO, int id)
        {
            var service = _serviceRepository.GetServiceById(id);
            if (service == null) return new ResponseDTO() { code = 400, message = "Id is not valid"};

            service.name = serviceDTO.name;
            service.description = serviceDTO.description;
            service.price = serviceDTO.price;
            service.image = serviceDTO.image;

            _serviceRepository.UpdateService(service);
            if (_serviceRepository.IsSaveChanges()) return new ResponseDTO();
            return new ResponseDTO() { code = 400, message = "Update Faile" };
        }
    }
}
