using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooHackathonAPI.Entities;

namespace ZooHackathonAPI.Repository.ReportImageRepo
{
    public interface IReportImageRepository
    {
        void CreateListReportImage(List<ReportImage> reportImages);
    }
}
