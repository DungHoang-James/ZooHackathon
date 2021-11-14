using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooHackathonAPI.Models.Report;

namespace ZooHackathonAPI.Services.ReportServices
{
    public interface IReportService
    {
        List<ReportDTO> GetReportsByUserId(int userId);
        List<ReportDTO> GetReports();
        ReportDTO GetReport(int id);
    }
}
