using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooHackathonAPI.DatabaseContext;
using ZooHackathonAPI.Entities;
using ZooHackathonAPI.Models.Statistics;

namespace ZooHackathonAPI.Repository.UserRepo
{
    public class UserRepository : IUserRepository
    {
        private readonly ZooDBContext _dBContext;

        public UserRepository(ZooDBContext dBContext)
        {
            this._dBContext = dBContext;
        }

        public List<UserReportDTO> GetStatisticUserReport()
        {
            return _dBContext.Users.Include(u => u.Reports).Select(u => new UserReportDTO() { UserID = u.ID, TotalReport = u.Reports.Count }).ToList();
        }
    }
}
