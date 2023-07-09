using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
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


        public IActionResult Index(string CourseId)
        {
            ViewData["CourseList"] = _courseRepository.GetList();
            ViewData["TeamList"] = _teamRepository.GetList();


            if (CourseId == null) CourseId = string.Empty;

            var teamList = _teamRepository.GetList();
            List<Team> teams = new List<Team>();

            foreach (Team team in teamList)
            {
                if (team.CourseId.Contains(CourseId)) teams.Add(team);
            }

            return View(teams);
        }



        // Add
        public async Task<IActionResult> AddTeam()
        {
            var courseList = _courseRepository.GetList();
            ViewData["CourseList"] = courseList;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTeam(string id, string CourseId)
        {
            AddTeam();

            // Validation
            bool returnSwitch = false;

            if (!new Validator().validate(id, @"^[T]\d*$"))
            {
                ViewData["Invalid"] = "Invalid";
                returnSwitch = true;
            }

            if (id.Length > 50)
            {
                ViewData["IdLength"] = "IdLength";
                returnSwitch = true;
            }

            if(returnSwitch) return View();

            // Check for Duplicate
            var course =_courseRepository.GetById(CourseId);
            var existingTeam = _teamRepository.GetById(id);
            if (existingTeam != null)
            {
                ViewBag.Result = "Duplicate";
                return View();
            }
            Team team = new Team();
            team.Course = course;
            team.CourseId = CourseId;
            team.Id = id;
            _teamRepository.Add(team);
            return RedirectToAction("Index");
        }

        // Update
        public async Task<IActionResult> UpdateTeam(string id)
        {
            var courseList = _courseRepository.GetList();
            ViewData["CourseList"] = courseList;

            var team = _teamRepository.GetById(id);
            return View(team);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTeam(string id, string CourseId)
        {
            UpdateTeam(id);

            var course = _courseRepository.GetById(CourseId);
            var team = _teamRepository.GetById(id);
            //if (team != null)
            //{
            //    ViewBag.Result = "Duplicate";
            //    return View(team);
            //}
            team.Course = course;
            team.CourseId = CourseId;
           _teamRepository.Update(team);

            return RedirectToAction("Index");
        }

        // Delete
        [HttpPost]
        public async Task<IActionResult> DeleteTeam(string id)
        {
            _teamRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
