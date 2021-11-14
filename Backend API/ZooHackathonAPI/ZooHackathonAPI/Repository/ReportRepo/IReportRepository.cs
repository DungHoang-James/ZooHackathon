using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooHackathonAPI.Entities;

namespace ZooHackathonAPI.Repository.ReportRepo
{
    public interface IReportRepository
    {
        List<Report> GetReportsByUserID(int userId);
        List<Report> GetReports();
        Report GetReport(int id);
    }
}
