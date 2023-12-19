using BongOliver.Models;
using Microsoft.EntityFrameworkCore;

namespace BongOliver.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void CreateUser(User user)
        {
            _dataContext.Users.Add(user);
        }
        public int GetTotal()
        {
            return _dataContext.Users.Count();
        }

        public void DeleteUser(User user)
        {
            var walet = _dataContext.Walets.Find(user.waletId);
            _dataContext.Walets.Remove(walet);
            _dataContext.Users.Remove(user);
        }

        public User GetUserById(int id)
        {
            return _dataContext.Users.Include(w => w.Walet).Include("role").FirstOrDefault(u => u.id == id);
        }
        public User GetUserByUsername(string username)
        {
            return _dataContext.Users.Include(w => w.Walet).Include("role").FirstOrDefault(u => u.username == username);
        }

        public List<User> GetUsers(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id")
        {
            //if (page * pageSize > _dataContext.Users.Count()) return null;

            var query = _dataContext.Users.Include(w => w.Walet).Include("role").AsQueryable();

            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(u => u.name.ToLower().Contains(key.ToLower()));
            }

            switch (sortBy)
            {
                case "name":
                    query = query.OrderBy(u => u.name);
                    break;
                default:
                    query = query.OrderBy(u => u.isDelete).ThenBy(u => u.id);
                    break;
            }
            if (page == null || pageSize == null || sortBy == null) { return query.ToList(); }
            else
                return query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();
        }
        public List<User> GetUsers()
        {
            var query = _dataContext.Users.Include(w => w.Walet).Include("role").ToList();
            return query;
        }
        public void UpdateUser(User user)
        {
            _dataContext.Entry(user).State = EntityState.Modified;
        }

        public bool IsSaveChanges()
        {
            return _dataContext.SaveChanges() > 0;
        }

        public List<User> GetStylist()
        {
            return _dataContext.Users.Include(w => w.Walet).Include(r => r.role).Where(u => u.role.id == 2).ToList();
        }
        public bool EmailIsValid(string email)
        {
            var user = _dataContext.Users.FirstOrDefault(u => u.email == email);
            if (user != null) return false;
            return true;
        }

        public User GetUserByEmail(string email)
        {
            return _dataContext.Users.FirstOrDefault(u => u.email == email);
        }
    }
}
