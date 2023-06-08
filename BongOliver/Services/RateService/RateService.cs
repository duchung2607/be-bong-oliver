using AutoMapper;
using BongOliver.DTOs.Rate;
using BongOliver.DTOs.Response;
using BongOliver.Models;
using BongOliver.Repositories.ItemRepository;
using BongOliver.Repositories.RateRepository;
using BongOliver.Repositories.ServiceRepository;
using BongOliver.Repositories.UserRepository;
using BongOliver.Services.UserService;

namespace BongOliver.Services.RateService
{
    public class RateService : IRateService
    {
        private readonly IMapper _mapper;
        private readonly IRateRepository _rateRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IUserRepository _userRepository;
        public RateService(IMapper mapper, IRateRepository rateRepository, IServiceRepository serviceRepository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _rateRepository = rateRepository;
            _serviceRepository = serviceRepository;
            _userRepository = userRepository;
        }
        public ResponseDTO CreateRate(CreateRateDTO createRateDTO)
        {
            if (createRateDTO.rate < 1 || createRateDTO.rate > 5) return new ResponseDTO()
            {
                code = 400,
                message = "Vui lòng đánh giá từ 1 - 5"
            };
            var user = _userRepository.GetUserById(createRateDTO.userId);
            if (user == null || user.role.name != "user") return new ResponseDTO()
            {
                code = 400,
                message = "User không hợp lệ"
            };

            var service = _serviceRepository.GetServiceById(createRateDTO.serviceId);
            if (service == null) return new ResponseDTO()
            {
                code = 400,
                message = "Sevice không tồn tại"
            };

            var rate = _mapper.Map<Rate>(createRateDTO);
            _rateRepository.CreateRate(rate);
            if (_rateRepository.IsSaveChanges()) return new ResponseDTO() { message = "Đánh giá thành công" };
            else return new ResponseDTO() { message = "Đánh giá thất bại" };
        }

    public ResponseDTO DeleteRate(int id)
    {
        throw new NotImplementedException();
    }

    public ResponseDTO GetRateById(int id)
    {
        var rate = _rateRepository.GetRateById(id);
        if (rate == null) return new ResponseDTO()
        {
            code = 400,
            message = "Id is not valid"
        };

        return new ResponseDTO()
        {
            data = _mapper.Map<RateDTO>(rate)
        };
    }

    public ResponseDTO GetRates()
    {
        var rates = _rateRepository.GetRates();
        var rateDTOs = rates.Select(_mapper.Map<Rate, RateDTO>).ToList();
        return new ResponseDTO()
        {
            data = rateDTOs,
        };
    }

    public ResponseDTO UpdateRate(RateDTO rateDTO)
    {
        throw new NotImplementedException();
    }
}
}
