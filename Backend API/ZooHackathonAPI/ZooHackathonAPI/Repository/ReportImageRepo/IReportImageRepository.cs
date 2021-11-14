using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooHackathonAPI.Entities;
using ZooHackathonAPI.Models.Statistics;

namespace ZooHackathonAPI.Repository.ReportImageRepo
{
    public interface IReportImageRepository
    {
        void CreateListReportImage(List<ReportImage> reportImages);
        double GetAveragePercentCorrect();
        int GetAmountOfImageByPercent(int percentFrom, int percentTo);
    }
}
