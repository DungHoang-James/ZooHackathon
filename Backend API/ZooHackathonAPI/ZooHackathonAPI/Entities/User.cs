using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZooHackathonAPI.Entities
{
    public class User
    {
        public int ID { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(30)]
        public string Password { get; set; }

        [MaxLength(100)]
        public string Fullname { get; set; }

        public int Role { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
