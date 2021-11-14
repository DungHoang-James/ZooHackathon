using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooHackathonAPI.DatabaseContext;
using ZooHackathonAPI.Entities;

namespace ZooHackathonAPI.Repository.ReportImageRepo
{
    public class ReportImageRepository : IReportImageRepository
    {
        private readonly ZooDBContext _dBContext;

        public ReportImageRepository(ZooDBContext dBContext)
        {
            this._dBContext = dBContext;
        }

        public void CreateListReportImage(List<ReportImage> reportImages)
        {
            _dBContext.ReportImages.AddRange(reportImages);
            _dBContext.SaveChanges();
        }
    }
}
