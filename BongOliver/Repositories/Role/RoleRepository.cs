using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BongOliver.Models;

namespace BongOliver.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;
 
        public RoleRepository(DataContext context)
        {
            _context = context;
        }

        public void CreateRole(Role role)
        {
            _context.Roles.Add(role);
        }

        public Role GetRoleById(int id)
        {
            return _context.Roles.FirstOrDefault(role => role.id == id);
        }
 
        public List<Role> GetRoles()
        {
            Console.WriteLine(_context.Roles.ToList());
            return _context.Roles.ToList();
        }
 
        public bool IsSaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
