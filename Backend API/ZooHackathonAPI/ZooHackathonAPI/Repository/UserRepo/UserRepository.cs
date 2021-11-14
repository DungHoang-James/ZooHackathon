using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooHackathonAPI.Entities;
using ZooHackathonAPI.Models.User;
using ZooHackathonAPI.Repository.BaseRepo;
using ZooHackathonAPI.DatabaseContext;
using ZooHackathonAPI.Models.Statistics;

namespace ZooHackathonAPI.Repository.UserRepo
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ZooDBContext _dBContext;

        public UserRepository(ZooDBContext dBContext) : base(dBContext)
        {
            this._dBContext = dBContext;
        }

        public int GetTotalUser()
        {
            return _dBContext.Users.Count();
        }
    }
}
