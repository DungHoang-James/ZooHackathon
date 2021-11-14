using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooHackathonAPI.Responses
{
    public class UserResponse
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool IsHideInfo { get; set; }
        public int ReportCount { get; set; }
    }
}
