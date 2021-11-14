using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooHackathonAPI.Entities;

namespace ZooHackathonAPI.Repository.ReportTextRepo
{
    public interface IReportTextRepository
    {
        void CreateListReportText(List<ReportText> reportTexts);
    }
}
