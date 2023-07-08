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



        public AssignmentStudentController(IAssignmentStudentRepository assignmentStudentRepository,
                                           IStudentRepository studentRepository,
                                           IAssignmentRepository assignmentRepository)
        {
            _assignmentStudentRepository = assignmentStudentRepository;
            _assignmentRepository = assignmentRepository;
            _studentRepository = studentRepository;
        }
  
       
        
        public async Task<IActionResult> Index()
        {
            var assignmentStudents = _assignmentStudentRepository.GetList().ToList();

            ViewData["AssignmentStudent"] = assignmentStudents;
            return View(assignmentStudents);
        }


        //Get two list and add it to viewdata
        public async Task<IActionResult> AddAssignmentStudent()
        {
            var assignmentList = _assignmentRepository.GetList();
            ViewData["AssignList"] = assignmentList;


            var studentList = _studentRepository.GetList();
            ViewData["StudentList"] = studentList;
           
            return View();
        }
        // Add
        [HttpPost]
        public async Task<IActionResult> AddAssignmentStudent(string taskId, string studentId, string Status)
        {
            Student student = new Student();
            AssignmentStudent assignmentStudent = new AssignmentStudent();
            Assignment assignment = new Assignment();
            AddAssignmentStudent();
            assignment = _assignmentRepository.GetById(taskId);
            student = _studentRepository.GetById(studentId);

            var existing = _assignmentStudentRepository.GetList().Where(p => p.TaskId.Equals(taskId)
                                                                          && p.StudentId.Equals(studentId)).FirstOrDefault();
            if(existing != null)
            {
                ViewBag.Result = "Duplicate";
                return View();
            }
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
        public async Task<IActionResult> UpdateStudentAssignment(int Id)
        {

            var assignmentList = _assignmentRepository.GetList();
            ViewData["AssignList"] = assignmentList;


            var studentList = _studentRepository.GetList();
            ViewData["StudentList"] = studentList;

            AssignmentStudent assignmentStudent = new AssignmentStudent();
            assignmentStudent = _assignmentStudentRepository.GetById(Id);
            return View(assignmentStudent);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStudentAssignment(int id, string taskId, string studentId, string Status)
        {
            
            var student = _studentRepository.GetById(studentId);
            var task = _assignmentRepository.GetById(taskId);
            //If duplicate exists, these list are neccessary so UpdateStudentAssignment page won't return null in AssignmentList/StudentList Viewdata
            UpdateStudentAssignment(id);

            AssignmentStudent assignmentStudent = new AssignmentStudent();
            assignmentStudent = _assignmentStudentRepository.GetById(id);
            if (assignmentStudent != null) {
                var existing = _assignmentStudentRepository.GetList().Where(p => p.TaskId.Equals(taskId)
                                                                              && p.StudentId.Equals(studentId)).FirstOrDefault();
                if (existing != null && existing.Id != assignmentStudent.Id) {
                    ViewBag.Result = "Duplicate";
                    return View(assignmentStudent);
                }
                assignmentStudent.StudentId = studentId;
                assignmentStudent.Status = Status;
                assignmentStudent.TaskId = taskId;
                assignmentStudent.Student = student;
                assignmentStudent.Task = task;
            }
            

            _assignmentStudentRepository.Update(assignmentStudent);
            return RedirectToAction("Index");
        }

        // Delete
        [HttpPost]
		public async Task<IActionResult> DeleteAssignmentStudent(int id)
		{
            _assignmentStudentRepository.Delete(id);
			return RedirectToAction("Index");
		}
	}
 }
    
