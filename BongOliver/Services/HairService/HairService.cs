using AutoMapper;
using BongOliver.DTOs.Hair;
using BongOliver.DTOs.Rate;
using BongOliver.DTOs.Response;
using BongOliver.Models;
using BongOliver.Repositories.HairRepository;
using BongOliver.Repositories.UserRepository;

namespace BongOliver.Services.HairService
{
    public class HairService : IHairService
    {
        private readonly IHairRepository _hairRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public HairService(IHairRepository hairRepository, IMapper mapper, IUserRepository userRepository)
        {
            _hairRepository = hairRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public ResponseDTO CreateHairStyle(HairStyleDTO hairStyleDTO)
        {
            var hairstyle = _mapper.Map<HairStyle>(hairStyleDTO);
            _hairRepository.CreateHairStyle(hairstyle);
            if (_hairRepository.IsSaveChanges()) return new ResponseDTO() { message = "Success" };
            else return new ResponseDTO()
            {
                code = 400,
                message = "Faile"
            };
        }

        public ResponseDTO DeleteHairStyle(int id)
        {
            var hairStyle = _hairRepository.GetHairStyleById(id);
            if (hairStyle == null) return new ResponseDTO() { code = 400, message = "Kiểu tóc không tồn tại" };

            _hairRepository.DeleteHairStyle(hairStyle);
            if (_hairRepository.IsSaveChanges()) return new ResponseDTO() { message = "Success" };
            else return new ResponseDTO()
            {
                code = 400,
                message = "Faile"
            };
        }

        public ResponseDTO GetAllHairStyles(int? page = 1, int? pageSize = 10, string? key = "")
        {
            return new ResponseDTO()
            {
                data = _hairRepository.GetAllHairStyles(page, pageSize, key).ToList(),
                total = _hairRepository.GetTotal()
                //data = _hairRepository.GetAllHairStyles().Select(_mapper.Map<HairStyle, HairStyleDTO>).ToList()
            };
        }

        public ResponseDTO GetHairStyleById(int id)
        {
            var hairStyle = _hairRepository.GetHairStyleById(id);
            if (hairStyle == null) return new ResponseDTO() { code = 400, message = "Kiểu tóc không tồn tại" };
            return new ResponseDTO()
            {
                data = _mapper.Map<HairStyleDTO>(hairStyle),
            };
        }

        public ResponseDTO UpdateHairStyle(HairStyleDTO hairStyleDTO, int id)
        {
            var hair = _hairRepository.GetHairStyleById(id);
            if (hair == null) return new ResponseDTO() { code = 400, message = "Kiểu tóc không tồn tại" };

            hair.name = hairStyleDTO.name;
            hair.description = hairStyleDTO.description;
            hair.image = hairStyleDTO.image;
            hair.sortDes = hairStyleDTO.sortDes;
            hair.type = hairStyleDTO.type;

            _hairRepository.UpdateHairStyle(hair);
            if (_hairRepository.IsSaveChanges()) return new ResponseDTO() { message = "Success" };
            else return new ResponseDTO() { code = 400, message = "Faile" };
        }
        
    }
}
