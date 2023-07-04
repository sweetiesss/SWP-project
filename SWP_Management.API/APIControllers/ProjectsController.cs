using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;

namespace SWP_Management.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [HttpGet]
        public IActionResult GetProjects()
        {
            var projects = _projectRepository.GetList();
            return Ok(projects);
        }

        [HttpGet("{projectId}")]
        public IActionResult GetProject(int projectId)
        {
            var project = _projectRepository.GetById(projectId);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [HttpPost]
        public IActionResult CreateProject(Project project)
        {
            _projectRepository.Add(project);
            return CreatedAtAction(nameof(GetProject), new { projectId = project.Id }, project);
        }

        [HttpPut("{projectId}")]
        public IActionResult UpdateProject(int projectId, Project project)
        {
            if (projectId != project.Id)
            {
                return BadRequest();
            }

            var existingProject = _projectRepository.GetById(projectId);
            if (existingProject == null)
            {
                return NotFound();
            }

            _projectRepository.Update(project);
            return NoContent();
        }

        [HttpDelete("{projectId}")]
        public IActionResult DeleteProject(int projectId)
        {
            var project = _projectRepository.GetById(projectId);
            if (project == null)
            {
                return NotFound();
            }

            _projectRepository.Delete(projectId);
            return NoContent();
        }
    }
}
