using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooHackathonAPI.Models.Report;
using ZooHackathonAPI.Services.ReportServices;

namespace ZooHackathonAPI.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            this._reportService = reportService;
        }

        [HttpGet]
        public ActionResult<List<ReportDTO>> GetReports()
        {
            var result = _reportService.GetReports();
            return Ok(result);
        }

        [HttpGet("id")]
        public ActionResult<List<ReportDTO>> GetReportById(int id)
        {
            var result = _reportService.GetReport(id);
            return Ok(result);
        }

        //[HttpPost]
        //public IActionResult CreateReport()
        //{

        //}
    }
}
