using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooHackathonAPI.Models.ReportImage;
using ZooHackathonAPI.Models.ReportText;

namespace ZooHackathonAPI.Models.Report
{
    public class ReportPayload
    {
        public int? UserID { get; set; } = null;
        public string DeviceID { get; set; }
        public List<ReportImageDTO> ReportImages { get; set; }
        public List<ReportTextDTO> ReportTexts { get; set; }
    }
}
