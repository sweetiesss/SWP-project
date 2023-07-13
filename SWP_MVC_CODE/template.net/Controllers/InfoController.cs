using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Repositories;
using System.Runtime.CompilerServices;

namespace testtemplate.Controllers
{
    public class InfoController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ILecturerRepository _lecturerRepository;
        private readonly IStudentTeamRepository _studentTeamRepository;
        private readonly ITeamRepository _teamRepository;


        public InfoController(IStudentRepository studentRepository, ILecturerRepository lecturerRepository, IStudentTeamRepository studentTeamRepository, ITeamRepository teamRepository)
        {
            _studentRepository = studentRepository;
            _lecturerRepository = lecturerRepository;
            _studentTeamRepository = studentTeamRepository;
            _teamRepository = teamRepository;
        }

        public IActionResult Index()
        {
            string id = ReadCookie();
            var student = _studentRepository.GetById(id);
            if(student != null)
            {
                ViewData["Student"] = student;
                return View("StudentInfo");
            }
            var lecturer = _lecturerRepository.GetById(id);
            if(lecturer != null) {
                ViewData["Lecturer"] = lecturer;
                return View("LecturerInfo");
            }
            

            return View();
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
