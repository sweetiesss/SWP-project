using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;

namespace SWP_Management.API.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportRepository _reportRepository;
        private readonly ITeamRepository _teamRepository;

        public ReportController(IReportRepository reportRepository,
                                 ITeamRepository teamRepository)
        {
            _reportRepository = reportRepository;
            _teamRepository = teamRepository;
        }


        public IActionResult Index()
        {
            var reportList = _reportRepository.GetList();
            return View(reportList);
        }


        // Add

        public async Task<IActionResult> AddReport()
        {
            var team = _teamRepository.GetList().ToList();
            ViewData["TeamList"] = team; 

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddReport(string TeamId, string id, string Descripton)
        {
            AddReport();


            // Validation
            bool returnSwitch = false;

            if (!new Validator().validate(id, @"^RP\d*$"))
            {
                ViewData["Invalid"] = "Invalid";
                returnSwitch = true;
            }

            if(id.Length > 50)
            {
                ViewData["IdLength"] = "IdLength";
                returnSwitch = true;
            }

            if (Descripton.Length > 200)
            {
                ViewData["DescriptionLength"] = "DescriptionLength";
                returnSwitch = true;
            }

            if (returnSwitch) return View();
            // Check for Duplicate
            var team = _teamRepository.GetById(TeamId);
            var existingReport = _reportRepository.GetById(id);
            if (existingReport != null)
            {
                ViewBag.Result = "Duplicate";
                return View();
            }
            Report report = new Report();
            report.TeamId = TeamId;
            report.Id = id;
            report.Description = Descripton;
            report.Team = team;

            _reportRepository.Add(report);
            return RedirectToAction("Index");
        }


        // Update
        public async Task<IActionResult> UpdateReport(string id)
        {
            var team = _teamRepository.GetList().ToList();
            ViewData["TeamList"] =team;

            var report= _reportRepository.GetById(id);
            return View(report);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateReport(string id, string TeamId, string descr)
        {
            UpdateReport(id);

            //Validation
            bool returnSwitch = false;

            if (descr.Length > 200)
            {
                ViewData["DescriptionLength"] = "DescriptionLength";
                returnSwitch = true;
            }
            if (returnSwitch) return View(_reportRepository.GetById(id));

            var team = _teamRepository.GetById(TeamId);
            var report = _reportRepository.GetById(id);
            report.Team = team;
            report.TeamId=TeamId;
            report.Description= descr;

            _reportRepository.Update(report);
            return RedirectToAction("Index");
            }
        

    // Delete
    [HttpPost]
    public async Task<IActionResult> DeleteReport(string id)
    {
            _reportRepository.Delete(id);
            return RedirectToAction("Index");
    }
    }
}
