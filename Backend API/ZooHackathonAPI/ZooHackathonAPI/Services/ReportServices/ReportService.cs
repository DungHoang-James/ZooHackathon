using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooHackathonAPI.Models.Report;
using ZooHackathonAPI.Repository.ReportRepo;

namespace ZooHackathonAPI.Services.ReportServices
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;

        public ReportService(IReportRepository reportRepository, IMapper mapper)
        {
            this._reportRepository = reportRepository;
            this._mapper = mapper;
        }

        public List<ReportDTO> GetReportsByUserId(int userId)
        {
            var reports = _reportRepository.GetReportsByUserID(userId);
            return _mapper.Map<List<ReportDTO>>(reports);
        }

        public List<ReportDTO> GetReports()
        {
            var reports = _reportRepository.GetReports();
            return _mapper.Map<List<ReportDTO>>(reports);
        }

        public ReportDTO GetReport(int id)
        {
            var report = _reportRepository.GetReport(id);
            return _mapper.Map<ReportDTO>(report);
        }
    }
}
