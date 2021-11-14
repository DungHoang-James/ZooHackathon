using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooHackathonAPI.Entities;
using ZooHackathonAPI.Models.ReportImage;
using ZooHackathonAPI.Models.ReportText;

namespace ZooHackathonAPI.Models.Report
{
    public class ReportDTO
    {
        public int ID { get; set; }
        public int? UserID { get; set; }
        public DateTime CreateDate { get; set; }
        public string DeviceID { get; set; }
        public List<ReportImageDTO> ReportImages { get; set; }
        public List<ReportTextDTO> ReportTexts { get; set; }
    }
}
