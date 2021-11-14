using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooHackathonAPI.Entities;
using ZooHackathonAPI.Models.User;
using ZooHackathonAPI.Repository.BaseRepo;

namespace ZooHackathonAPI.Repository.UserRepo
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
