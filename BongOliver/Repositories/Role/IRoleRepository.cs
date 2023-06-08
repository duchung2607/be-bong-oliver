using BongOliver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BongOliver.Repositories
{
    public interface IRoleRepository
    {
        List<Role> GetRoles();
        Role GetRoleById(int id);
        void CreateRole(Role role);
        bool IsSaveChanges();
    }
}