using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooHackathonAPI.ViewModels
{
    public class TokenViewModel
    {
        public TokenViewModel(int id, int role)
        {
            Id = id;
            Role = role;
        }

        public int Id { get; set; }
        public int Role { get; set; }
    }
}
