using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooHackathonAPI.Requests
{
    public class UpdateUserRequest
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public bool IsHideInfo { get; set; }
    }
}
