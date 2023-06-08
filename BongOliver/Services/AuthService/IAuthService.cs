using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BongOliver.DTOs.Response;
using BongOliver.DTOs.User;

namespace BongOliver.Services.AuthService
{
    public interface IAuthService
    {
        //string Login(AuthUserDTO authUserDTO);
        ResponseDTO Register(RegisterUserDTO registerUserDTO);

        ResponseDTO Login(AuthUserDTO authUserDTO);

        //ResponseDTO SendEmail(string to,string subject, string body);
        ResponseDTO ForgotPassword(string email);
        ResponseDTO VerifyEmail(string email, string token);
        //ResponseDTO Register(RegisterUserDTO registerUserDTO);
    }
}