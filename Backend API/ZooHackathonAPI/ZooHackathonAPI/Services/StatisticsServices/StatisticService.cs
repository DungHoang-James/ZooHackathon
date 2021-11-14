using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooHackathonAPI.Models.Statistics;
using ZooHackathonAPI.Repository.ReportImageRepo;
using ZooHackathonAPI.Repository.ReportRepo;
using ZooHackathonAPI.Repository.UserRepo;

namespace ZooHackathonAPI.Services.StatisticsServices
{
    public class StatisticService : IStatisticService
    {
        private readonly IUserRepository _userRepository;
        private readonly IReportRepository _reportRepository;
        private readonly IReportImageRepository _reportImageRepository;

        public StatisticService(IUserRepository userRepository,
                                IReportRepository reportRepository,
                                IReportImageRepository reportImageRepository)
        {
            this._userRepository = userRepository;
            this._reportRepository = reportRepository;
            this._reportImageRepository = reportImageRepository;
        }

        public UserReportDTO GetStatisticUserReport()
        {
            var totalUser = _userRepository.GetTotalUser();
            var totalReport = _reportRepository.GetTotalReport();
            var totalDevice = _reportRepository.GetTotalDevice();
            var average = _reportImageRepository.GetAveragePercentCorrect();

            List<AmountImagePercentCorrect> percentCorrects = new()
            {
                new AmountImagePercentCorrect() { PercentFrom = 0, PercentTo = 25 },
                new AmountImagePercentCorrect() { PercentFrom = 26, PercentTo = 50 },
                new AmountImagePercentCorrect() { PercentFrom = 51, PercentTo = 75 },
                new AmountImagePercentCorrect() { PercentFrom = 76, PercentTo = 100 },
            };

            foreach (var item in percentCorrects)
            {
                item.AmountOfImage = _reportImageRepository.GetAmountOfImageByPercent(item.PercentFrom, item.PercentTo);
            }

            UserReportDTO userReportDTO = new()
            {
                TotalReport = totalReport,
                TotalUser = totalUser,
                TotalDevice = totalDevice,
                AveragePercentCorrect = average,
                ImagePercentCorrects = percentCorrects,
            };
            return userReportDTO;
        }
    }
}
