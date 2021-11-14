using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZooHackathonAPI.Entities
{
    public class ReportImage
    {
        [Key]
        public int ID { get; set; }
        public int ReportID { get; set; }
        public string ImageURL { get; set; }
        public double PercentCorrect { get; set; }

        [ForeignKey(nameof(ReportID))]
        public Report Report { get; set; }
    }
}
