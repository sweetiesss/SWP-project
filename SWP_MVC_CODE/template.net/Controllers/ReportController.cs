using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;

namespace testtemplate.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportRepository _reportRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IStudentTeamRepository _studentTeamRepository;
        private readonly IStudentRepository _studentRepository;

        public ReportController(IReportRepository reportRepository, ITeamRepository teamRepository, IStudentTeamRepository studentTeamRepository, IStudentRepository studentRepository)
        {
            _reportRepository = reportRepository;
            _teamRepository = teamRepository;
            _studentTeamRepository = studentTeamRepository;
            _studentRepository = studentRepository;
        }

        public IActionResult Index()
        {
            string id = ReadCookie();
            var team = _studentTeamRepository.GetList().Where(p => p.StudentId.Equals(id)).FirstOrDefault();
            var listReport = _reportRepository.GetList().Where(p => p.TeamId.Equals(team.TeamId)).ToList();

            ViewData["ReportList"] = listReport;
            ViewData["cookieValue"] = id;
            return View();
        }

        public IActionResult AddReport(string id)
        {
            var team = _studentTeamRepository.GetList().Where(p => p.StudentId.Equals(id)).FirstOrDefault();
            ViewData["Cookie"] = id;
            ViewData["TeamId"] = team;
            return View();
        }

        [HttpPost]
        public IActionResult AddReport(string id,string descripton, string teamId)
        {
            string studentId = ReadCookie();
            AddReport(studentId);
            var team = _teamRepository.GetById(teamId);
            var existing = _reportRepository.GetById(id);
            if (existing != null)
            {
                ViewBag.Result = "Duplicate";
                return View();
            }
            Report report = new Report();
            report.Id = id;
            report.Description = descripton;
            report.TeamId = teamId;
            report.Team = team;

            _reportRepository.Add(report);
            return RedirectToAction("Index");

        }


        public IActionResult UpdateReport(string id)
        {
            var report = _reportRepository.GetById(id);
            ViewData["Report"] = report;
            return View();
        }

        [HttpPost]
        public IActionResult UpdateReport(string id, string descripton, string teamId)
        {
            var report = _reportRepository.GetById(id);
            report.Description = descripton;
            report.TeamId = teamId;
            //report.Team = _teamRepository.GetById(TeamId);
            _reportRepository.Update(report);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult DeleteReport(string id)
        {
            _reportRepository.Delete(id); 
            return RedirectToAction("Index");
        }

        public string ReadCookie()
        {
            String key = "User";
            var cookieValue = Request.Cookies[key];
            ViewData["cookieValue"] = cookieValue;
            return cookieValue;

        }
    }
}
