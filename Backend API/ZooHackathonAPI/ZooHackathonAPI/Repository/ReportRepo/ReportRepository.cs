using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooHackathonAPI.DatabaseContext;
using ZooHackathonAPI.Entities;
using ZooHackathonAPI.Models.Report;

namespace ZooHackathonAPI.Repository.ReportRepo
{
    public class ReportRepository : IReportRepository
    {
        private readonly ZooDBContext _dBContext;

        public ReportRepository(ZooDBContext dBContext)
        {
            this._dBContext = dBContext;
        }

        public List<Report> GetReportsByUserID(int userId)
        {
            return _dBContext.Reports.Where(r => r.UserID == userId).ToList();
        }

        public List<Report> GetReports()
        {
            return _dBContext.Reports.ToList();
        }

        public Report GetReport(int id)
        {
            return _dBContext.Reports.Include(r => r.ReportImages).Include(r => r.ReportTexts).FirstOrDefault(r => r.ID == id);
        }
    }
}
