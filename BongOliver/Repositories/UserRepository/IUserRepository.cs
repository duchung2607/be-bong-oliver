using BongOliver.Models;

namespace BongOliver.Repositories.UserRepository
{
    public interface IUserRepository
    {
        List<User> GetUsers(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id");
        List<User> GetUsers();
        User GetUserById(int id);
        User GetUserByUsername(string username);
        User GetUserByEmail(string email);
        int GetTotal();
        void UpdateUser(User user);
        void DeleteUser(User user);
        void CreateUser(User user);
        List<User> GetStylist();
        bool IsSaveChanges();
        bool EmailIsValid(string email);
    }
}
