using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooHackathonAPI.Entities;
using ZooHackathonAPI.Models.Report;
using ZooHackathonAPI.Repository.ReportImageRepo;
using ZooHackathonAPI.Repository.ReportRepo;
using ZooHackathonAPI.Repository.ReportTextRepo;

namespace ZooHackathonAPI.Services.ReportServices
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IReportTextRepository _reportTextRepository;
        private readonly IReportImageRepository _reportImageRepository;
        private readonly IMapper _mapper;

        public ReportService(IReportRepository reportRepository,
                             IReportTextRepository reportTextRepository,
                             IReportImageRepository reportImageRepository,
                             IMapper mapper)
        {
            this._reportRepository = reportRepository;
            this._reportTextRepository = reportTextRepository;
            this._reportImageRepository = reportImageRepository;
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

        public int CreateReport(ReportPayload reportPayload)
        {
            // create new report
            Report report = new Report
            {
                CreateDate = DateTime.Now,
                DeviceID = reportPayload.DeviceID,
            };

            // get userId from payload
            var userId = reportPayload.UserID;

            // check userId != null, mean user using their info to report
            if (userId != null)
            {
                report.UserID = reportPayload.UserID;
            }

            // persist report to database
            var result = _reportRepository.CreateReport(report);

            // result > 0, mean success
            if (result > 0)
            {
                // initial 2 variable List contain all report image and text
                List<ReportText> reportTexts = new();
                List<ReportImage> reportImages = new();

                // loop through List report image in payload and add to List
                foreach (var rI in reportPayload.ReportImages)
                {
                    var newRI = _mapper.Map<ReportImage>(rI);
                    newRI.ReportID = report.ID;
                    reportImages.Add(newRI);
                }

                // loop through List report text in payload and add to List
                foreach (var rt in reportPayload.ReportTexts)
                {
                    var newRT = _mapper.Map<ReportText>(rt);
                    newRT.ReportID = report.ID;
                    reportTexts.Add(newRT);
                }

                // persist to database
                _reportImageRepository.CreateListReportImage(reportImages);
                _reportTextRepository.CreateListReportText(reportTexts);

                return report.ID;
            }

            return 0;
        }
    }
}
