using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooHackathonAPI.DatabaseContext;
using ZooHackathonAPI.Entities;

namespace ZooHackathonAPI.Repository.ReportTextRepo
{
    public class ReportTextRepository : IReportTextRepository
    {
        private readonly ZooDBContext _dBContext;

        public ReportTextRepository(ZooDBContext dBContext)
        {
            this._dBContext = dBContext;
        }

        public void CreateListReportText(List<ReportText> reportTexts)
        {
            _dBContext.ReportTexts.AddRange(reportTexts);
            _dBContext.SaveChanges();
        }
    }
}
