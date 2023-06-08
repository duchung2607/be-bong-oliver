using BongOliver.Models;

namespace BongOliver.Services.TokenService
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
// , string firstname, string lastname, int role_id