using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Repositories;

namespace SWP_Management.API.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ICourseRepository _courseRepository;

        public TeamController(ITeamRepository teamRepository,
                                  ICourseRepository courseRepository)
        {
            _teamRepository = teamRepository;
            _courseRepository = courseRepository;
        }


        public IActionResult Index()
        {
            var teamList = _teamRepository.GetList();
            return View(teamList);
        }



        // Add
        // Update
        // Delete
    }
}
