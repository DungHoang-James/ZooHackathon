using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZooHackathonAPI.Entities
{
    public class Report
    {
        public int ID { get; set; }
        public int? UserID { get; set; }
        public DateTime CreateDate { get; set; }
        public string DeviceID { get; set; }

        [ForeignKey(nameof(UserID))]
        public User User { get; set; }

        public List<ReportImage> ReportImages { get; set; }
        public List<ReportText> ReportTexts { get; set; }
    }
}
