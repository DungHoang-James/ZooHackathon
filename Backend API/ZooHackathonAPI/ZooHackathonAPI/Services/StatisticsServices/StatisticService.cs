using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooHackathonAPI.Models.Statistics;
using ZooHackathonAPI.Repository.UserRepo;

namespace ZooHackathonAPI.Services.StatisticsServices
{
    public class StatisticService : IStatisticService
    {
        private readonly IUserRepository _userRepository;

        public StatisticService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public List<UserReportDTO> GetStatisticUserReport()
        {
            return _userRepository.GetStatisticUserReport();
        }
    }
}
