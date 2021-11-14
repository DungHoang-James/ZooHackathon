using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooHackathonAPI.Models.Statistics
{
    public class UserReportDTO
    {
        public int TotalUser { get; set; }
        public int TotalReport { get; set; }
        public int TotalDevice { get; set; }
        public double AveragePercentCorrect { get; set; }
        public List<AmountImagePercentCorrect> ImagePercentCorrects { get; set; }
    }
}
