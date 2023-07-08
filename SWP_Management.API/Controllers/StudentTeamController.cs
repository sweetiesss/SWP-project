using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;

namespace SWP_Management.API.Controllers
{
    public class StudentTeamController : Controller
    {
        private readonly IStudentTeamRepository _studentTeamRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IStudentRepository _studentRepository;

        public StudentTeamController(IStudentTeamRepository studentTeamRepository,
                                     ITeamRepository teamRepository,
                                     IStudentRepository studentRepository)
        {
            _studentTeamRepository = studentTeamRepository;
            _teamRepository = teamRepository;
            _studentRepository = studentRepository;
        }


        public IActionResult Index()
        {
            var studentTeamList = _studentTeamRepository.GetList();
            return View(studentTeamList);
        }



        // Add
        public async Task<IActionResult> AddStudentTeam()
        {
            var studentList = _studentRepository.GetList().ToList();
            ViewData["StudentList"] = studentList;

            var teamList = _teamRepository.GetList().ToList();
            ViewData["TeamList"] = teamList;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddStudentTeam(int id, string TeamId, string StudentId)
        {
            AddStudentTeam();
            var student = _studentRepository.GetById(StudentId);
            var team = _teamRepository.GetById(TeamId);

            //Since a student can only be in one team, it is not necessary to check for duplicate TeamId, instead
            //check for duplicate StudentId on the list, if it exists, the student is already assigned
            //so therefore it can't exist on another Team.
            var existingStudent = _studentTeamRepository.GetList().Where(p => p.StudentId.Equals(StudentId)).FirstOrDefault();
            if( existingStudent != null)
            {
                ViewBag.Result = "Duplicate";
                return View();
            }
      
            StudentTeam studentTeam = new StudentTeam();
            studentTeam.Student = student;
            studentTeam.Team = team;
            studentTeam.StudentId = StudentId;
            studentTeam.TeamId = TeamId;

            _studentTeamRepository.Add(studentTeam);
            return RedirectToAction("Index");
        }

        // Update
        public async Task<IActionResult> UpdateStudentTeam(int id)
        {
            var studentList = _studentRepository.GetList().ToList();
            ViewData["StudentList"] = studentList;

            var teamList = _teamRepository.GetList().ToList();
            ViewData["TeamList"] = teamList;

            var studentTeam = _studentTeamRepository.GetById(id);
            return View(studentTeam);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStudentTeam(int id, string TeamId, string StudentId)
        {
            UpdateStudentTeam(id);
            var student = _studentRepository.GetById(StudentId);
            var team = _teamRepository.GetById(TeamId);
            var studentTeam = _studentTeamRepository.GetById(id);

            var existingStudent = _studentTeamRepository.GetList().Where(p => p.StudentId.Equals(StudentId)).FirstOrDefault();
            if (existingStudent != null)
            {
                if(existingStudent.Id != studentTeam.Id)
                {
                    ViewBag.Result = "Duplicate";
                    return View(studentTeam);
                }
            }
            studentTeam.StudentId = StudentId;
            studentTeam.TeamId = TeamId;
            studentTeam.Team = team;
            studentTeam.Student = student;
            _studentTeamRepository.Update(studentTeam);
            return RedirectToAction("Index");
        }
        // Delete

        [HttpPost]
        public async Task<IActionResult> DeleteStudentTeam(int id)
        {
            _studentTeamRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
