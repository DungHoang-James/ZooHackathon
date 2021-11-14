using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooHackathonAPI.Entities;
using ZooHackathonAPI.Requests;
using ZooHackathonAPI.Responses;

namespace ZooHackathonAPI.Services.UserServices
{
    public interface IUserService
    {
        Task<UserResponse> Register(string email, string password, int role, bool isHideInfo);
        Task<UserResponse> Login(string email, string password);
        Task<User> UpdateUser(UpdateUserRequest request);
    }
}
