using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;

namespace testtemplate.Controllers
{
    public class AssignmentStudentController : Controller
    {

        private readonly IAssignmentStudenteRepository _assignmentStudentRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IAssignmentRepository _assignmentRepository;

        public AssignmentStudentController(IAssignmentStudenteRepository assignmentStudentRepository, IStudentRepository studentRepository, IAssignmentRepository assignmentRepository)
        {
            _assignmentStudentRepository = assignmentStudentRepository;
            _studentRepository = studentRepository;
            _assignmentRepository = assignmentRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public string ReadCookie()
        {
            String key = "User";
            var cookieValue = Request.Cookies[key];
            ViewData["cookieValue"] = cookieValue;
            return cookieValue;

        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int AssignmentStudentId, string Status, string taskId)
        {
            string studentId = ReadCookie();
            var student = _studentRepository.GetById(studentId);
            var assignment = _assignmentRepository.GetById(taskId);
            AssignmentStudente assignmentStudent = new AssignmentStudente();
            assignmentStudent = _assignmentStudentRepository.GetById(AssignmentStudentId);

            assignmentStudent.StudentId = studentId;
            assignmentStudent.Status = "Ongoing";
            assignmentStudent.TaskId = assignment.Id;
            assignmentStudent.Student = student;
            assignmentStudent.Task = assignment;



            _assignmentStudentRepository.Update(assignmentStudent);
            return RedirectToAction("Index");
        }
    }
}
