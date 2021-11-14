using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooHackathonAPI.Responses;

namespace ZooHackathonAPI.Services.UserServices
{
    public interface IUserService
    {
        Task<LoginResponse> Register(string email, string password, int role, bool isHideInfo);
        Task<LoginResponse> Login(string email, string password);
    }
}
