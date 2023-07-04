using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;
using System.Net.Http;
using System.Text;

namespace SWP_Frontend_Admin.Controllers
{
    public class AssignmentStudentController : Controller
    {
        private readonly IAssignmentStudentRepository _assignmentStudentRepository;
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IStudentRepository _studentRepository;



        public AssignmentStudentController(IAssignmentStudentRepository assignmentStudentRepository, IStudentRepository studentRepository, IAssignmentRepository assignmentRepository)
        {
            _assignmentStudentRepository = assignmentStudentRepository;
            _assignmentRepository = assignmentRepository;
            _studentRepository = studentRepository;
        }
  
       
        
        public async Task<IActionResult> Index()
        {
            List<AssignmentStudent> assignmentStudents = new List<AssignmentStudent>();
            var response = _assignmentStudentRepository.GetList().ToJson();
            assignmentStudents = JsonConvert.DeserializeObject<List<AssignmentStudent>>(response);

            ViewData["AssignmentStudent"] = assignmentStudents;
            return View(assignmentStudents);
        }


        //Get two list and add it to viewdata
        public async Task<IActionResult> AddAssignmentStudent()
        {
            List<Assignment> assignments = new List<Assignment>();
            List<Student> students = new List<Student>();
            
            var response = _assignmentRepository.GetList().ToJson();
            assignments = JsonConvert.DeserializeObject<List<Assignment>>(response);
            ViewData["AssignList"] = assignments;

            
            response = _studentRepository.GetList().ToJson();
            students = JsonConvert.DeserializeObject<List<Student>>(response);
            ViewData["StudentList"] = students;
           
            return View();
        }

        public ViewResult SelectAssignmentStudent() => View();


        // Add
        [HttpPost]
        public async Task<IActionResult> SelectAssignmentStudent(String taskId, String studentId, String Status)
        {
            Student student = new Student();
            AssignmentStudent assignmentStudent = new AssignmentStudent();
            Assignment assignment = new Assignment();

            assignment = _assignmentRepository.GetById(taskId);
            student = _studentRepository.GetById(studentId);
            // Insert data to an entity for add
            assignmentStudent.Task = assignment;
            assignmentStudent.Student = student;
            assignmentStudent.TaskId = taskId;
            assignmentStudent.StudentId = studentId;
            assignmentStudent.Status = Status;

            // add
            _assignmentStudentRepository.Add(assignmentStudent);
            

            return RedirectToAction("Index");
        }


        // Update
        public async Task<IActionResult> UpdateStudentAssignment(int Id, string studentId)
        {
            List<Assignment> assignments = new List<Assignment>();
            List<Student> students = new List<Student>();

            var response = _assignmentRepository.GetList().ToJson();
            assignments = JsonConvert.DeserializeObject<List<Assignment>>(response);
            ViewData["AssignList"] = assignments;


            response = _studentRepository.GetList().ToJson();
            students = JsonConvert.DeserializeObject<List<Student>>(response);
            ViewData["StudentList"] = students;

            AssignmentStudent assignmentStudent = new AssignmentStudent();
            assignmentStudent = _assignmentStudentRepository.GetById(Id);
            return View(assignmentStudent);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStudentAssignment(AssignmentStudent asmStu)
        {
            _assignmentStudentRepository.Update(asmStu);
            return RedirectToAction("Index");
        }

        // Check Duplicates AssignmentStudent that i dont use
        private bool checkDup(AssignmentStudent asm)
        {
            var AssignmentStudentList = _assignmentStudentRepository.GetList();
            bool isDuplicate = false;
            foreach (var checker in AssignmentStudentList)
            {
                if (asm.StudentId.Equals(checker.StudentId) && asm.TaskId.Equals(checker.TaskId)) return true;

            }
            return isDuplicate;
        }


        //Delete
        [HttpPost]
		public async Task<IActionResult> DeleteAssignmentStudent(string taskId, string studentId)
		{
            AssignmentStudent assignmentStudent = new AssignmentStudent();
            assignmentStudent = _assignmentStudentRepository.GetList().Where(p => p.TaskId.Equals(taskId) &&
                                                                                  p.StudentId.Equals(studentId)).FirstOrDefault();
            _assignmentStudentRepository.Delete(assignmentStudent.Id);
			return RedirectToAction("Index");
		}
	}
 }
    
