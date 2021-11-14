using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooHackathonAPI.Entities;
using ZooHackathonAPI.Models.User;
using ZooHackathonAPI.Requests;

namespace ZooHackathonAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

            CreateMap<User, UpdateUserRequest>();
            CreateMap<UpdateUserRequest, User>();
        }
    }
}
