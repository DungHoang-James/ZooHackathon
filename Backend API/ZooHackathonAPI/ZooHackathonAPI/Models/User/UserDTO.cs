﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooHackathonAPI.Models.User
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public int Role { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsHideInfo { get; set; }
    }
}
