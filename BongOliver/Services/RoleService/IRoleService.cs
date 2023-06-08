using BongOliver.DTOs.Role;

namespace BongOliver.Services.RoleService
{
    public interface IRoleService
    {
        List<RoleDTO> GetRoles();
        string CreateRole(RoleDTO roleDTO);
        RoleDTO GetRoleByUsername(string username);
    }
}
