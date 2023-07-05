using Microsoft.AspNetCore.Mvc;
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
        // Update
        // Delete
    }
}
