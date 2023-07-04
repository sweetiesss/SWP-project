using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;

namespace SWP_Management.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;

        public ReportController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        [HttpGet]
        public IActionResult GetReports()
        {
            var reports = _reportRepository.GetList();
            return Ok(reports);
        }

        [HttpGet("{reportId}")]
        public IActionResult GetReport(string reportId)
        {
            var report = _reportRepository.GetById(reportId);
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }

        [HttpPost]
        public IActionResult CreateReport(Report report)
        {
            _reportRepository.Add(report);
            return CreatedAtAction(nameof(GetReport), new { reportId = report.Id }, report);
        }

        [HttpPut("{reportId}")]
        public IActionResult UpdateReport(string reportId, Report report)
        {
            if (reportId != report.Id)
            {
                return BadRequest();
            }

            var existingReport = _reportRepository.GetById(reportId);
            if (existingReport == null)
            {
                return NotFound();
            }

            _reportRepository.Update(report);
            return NoContent();
        }

        [HttpDelete("{reportId}")]
        public IActionResult DeleteReport(string reportId)
        {
            var report = _reportRepository.GetById(reportId);
            if (report == null)
            {
                return NotFound();
            }

            _reportRepository.Delete(reportId);
            return NoContent();
        }
    }
}
