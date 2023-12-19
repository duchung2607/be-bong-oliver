using BongOliver.DTOs.Booking;
using BongOliver.DTOs.Response;
using BongOliver.DTOs.User;

namespace BongOliver.Services.UserService
{
    public interface IUserService
    {
        ResponseDTO GetUsers(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id");
        ResponseDTO GetUserById(int id);
        ResponseDTO GetUserByUsername(string username);
        ResponseDTO UpdateUser(UserDTO user);
        ResponseDTO PayIn(string username, double money);
        Task<ResponseDTO> PayMentWithWalet(string username, int bookingId);
        ResponseDTO DeleteUser(string username);
        ResponseDTO GetStylist();
        ResponseDTO CreateUser(CreateUserDTO createUser);
        bool IsSaveChanges();
        ResponseDTO UpdateUser(UpdateUser updateUser, string username);
        ResponseDTO UpdateWaletUser(string username,double money);
        bool EmailIsValid(string email);
        ResponseDTO VerifyEmail(string username);
        ResponseDTO ChangePass(ChangePassDTO changePassDTO, string username);
        ResponseDTO GetUserByIds(List<int> ids);
    }
}
