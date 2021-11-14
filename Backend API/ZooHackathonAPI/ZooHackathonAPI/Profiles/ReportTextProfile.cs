using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooHackathonAPI.Entities;
using ZooHackathonAPI.Models.ReportText;

namespace ZooHackathonAPI.Profiles
{
    public class ReportTextProfile : Profile
    {
        public ReportTextProfile()
        {
            CreateMap<ReportText, ReportTextDTO>();
        }
    }
}
