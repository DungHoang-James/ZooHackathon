using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooHackathonAPI.Models.Statistics;
using ZooHackathonAPI.Services.StatisticsServices;

namespace ZooHackathonAPI.Controllers
{
    [ApiController]
    [Route("api/statistics")]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticService _statisticService;

        public StatisticController(IStatisticService statisticService)
        {
            this._statisticService = statisticService;
        }

        [HttpGet]
        public ActionResult<List<UserReportDTO>> GetUserReportStatistic()
        {
            var result = _statisticService.GetStatisticUserReport();
            return Ok(result);
        }
    }
}
