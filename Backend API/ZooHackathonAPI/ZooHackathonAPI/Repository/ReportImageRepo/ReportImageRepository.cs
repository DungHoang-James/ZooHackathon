using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooHackathonAPI.DatabaseContext;
using ZooHackathonAPI.Entities;
using ZooHackathonAPI.Models.Statistics;

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

        public double GetAveragePercentCorrect()
        {
            var listIdReport = _dBContext.Reports.Select(r => r.ID).ToList();

            List<double> averageEachReport = new();

            foreach (var id in listIdReport)
            {
                var averageItem = _dBContext.ReportImages.Where(r => r.ID == id).Average(r => r.PercentCorrect);
                averageEachReport.Add(averageItem);
            }

            return averageEachReport.Average();
        }

        public int GetAmountOfImageByPercent(int percentFrom, int percentTo)
        {
            return _dBContext.ReportImages.Where(r => percentFrom <= r.PercentCorrect && r.PercentCorrect <= percentTo).Count();
        }
    }
}
