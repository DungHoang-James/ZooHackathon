using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZooHackathonAPI.Entities
{
    public class ReportText
    {
        [Key]
        public int ReportID { get; set; }
        public string Text { get; set; }

        [ForeignKey(nameof(ReportID))]
        public Report Report { get; set; }
    }
}
