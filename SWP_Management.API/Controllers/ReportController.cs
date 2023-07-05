using Microsoft.AspNetCore.Mvc;
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
        // Update
        // Delete

    }
}
