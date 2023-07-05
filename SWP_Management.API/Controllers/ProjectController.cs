using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Repositories;

namespace SWP_Management.API.Controllers
{
    public class ProjectController : Controller
    {

        private readonly IProjectRepository _projectRepository;
        private readonly ITopicRepository _topicRepository;
        private readonly ITeamRepository _teamRepository;

        public ProjectController(IProjectRepository projectRepository,
                                        ITopicRepository topicRepository,
                                        ITeamRepository teamRepository)
        {
            _projectRepository = projectRepository;
            _topicRepository = topicRepository;
            _teamRepository = teamRepository;
        }


        public IActionResult Index()
        {
            var projectList = _projectRepository.GetList();
            return View(projectList);
        }



        // Add
        // Update
        // Delete
    }
}
