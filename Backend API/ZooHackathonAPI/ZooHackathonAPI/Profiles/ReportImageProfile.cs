using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooHackathonAPI.Entities;
using ZooHackathonAPI.Models.ReportImage;

namespace ZooHackathonAPI.Profiles
{
    public class ReportImageProfile : Profile
    {
        public ReportImageProfile()
        {
            CreateMap<ReportImage, ReportImageDTO>();
        }
    }
}
