//using AutoMapper;
//using AutoMapper.QueryableExtensions;
using BongOliver.Models;
using BongOliver.DTOs.Role;
using BongOliver.Repositories;
using System.Security.Cryptography;
using System.Text;
using BongOliver.Services.TokenService;
using BongOliver.Repositories.UserRepository;

namespace BongOliver.Services.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly DataContext _context;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        // private readonly IMapper _mapper;
        public RoleService(DataContext context, IRoleRepository roleRepository)
        {
            _context = context;
            _roleRepository = roleRepository;
        }

        public string CreateRole(RoleDTO roleDTO)
        {
            var role = new Role(roleDTO.name);
            _roleRepository.CreateRole(role);

            if (_roleRepository.IsSaveChanges()) return "Create Success";
            else return "Create Error";
        }

        public RoleDTO GetRoleByUsername(string username)
        {
            var user = _userRepository.GetUserByUsername(username);
            var role = _roleRepository.GetRoleById(user.role.id);
            return new RoleDTO()
            {
                id= role.id,
                name = role.name
            };
        }

        public List<RoleDTO> GetRoles()
        {
            var rolesDto = new List<RoleDTO>();
            var roles = _roleRepository.GetRoles();
            foreach (var role in roles) 
            {
                rolesDto.Add(new RoleDTO()
                    {
                        id = role.id,
                        name = role.name
                    }
                );
            }
            return rolesDto;
        }

        public bool IsSaveChanges()
        {
            return _roleRepository.IsSaveChanges();
        }
    }
}