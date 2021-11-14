using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooHackathonAPI.Models.Statistics;

namespace ZooHackathonAPI.Services.StatisticsServices
{
    public interface IStatisticService
    {
        UserReportDTO GetStatisticUserReport();
    }
}
