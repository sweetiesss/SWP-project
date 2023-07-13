using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;

namespace SWP_Management.Controllers
{
    [ApiController]
    [Route("api/studentteams")]
    public class StudentTeamController : ControllerBase
    {
        private readonly IStudentTeamRepository _studentTeamRepository;

        public StudentTeamController(IStudentTeamRepository studentTeamRepository)
        {
            _studentTeamRepository = studentTeamRepository;
        }

        [HttpGet]
        public IActionResult GetStudentTeams()
        {
            var studentTeams = _studentTeamRepository.GetList();
            return Ok(studentTeams);
        }

        [HttpGet("{studentTeamId}")]
        public IActionResult GetStudentTeam(int studentTeamId)
        {
            var studentTeam = _studentTeamRepository.GetById(studentTeamId);
            if (studentTeam == null)
            {
                return NotFound();
            }
            return Ok(studentTeam);
        }

        [HttpPost]
        public IActionResult CreateStudentTeam(StudentTeam studentTeam)
        {
            _studentTeamRepository.Add(studentTeam);
            return CreatedAtAction(nameof(GetStudentTeam), new { studentTeamId = studentTeam.Id }, studentTeam);
        }

        [HttpPut("{studentTeamId}")]
        public IActionResult UpdateStudentTeam(int studentTeamId, StudentTeam studentTeam)
        {
            if (studentTeamId != studentTeam.Id)
            {
                return BadRequest();
            }

            var existingStudentTeam = _studentTeamRepository.GetById(studentTeamId);
            if (existingStudentTeam == null)
            {
                return NotFound();
            }

            _studentTeamRepository.Update(studentTeam);
            return NoContent();
        }

        [HttpDelete("{studentTeamId}")]
        public IActionResult DeleteStudentTeam(int studentTeamId)
        {
            var studentTeam = _studentTeamRepository.GetById(studentTeamId);
            if (studentTeam == null)
            {
                return NotFound();
            }

            _studentTeamRepository.Delete(studentTeamId);
            return NoContent();
        }
    }
}
