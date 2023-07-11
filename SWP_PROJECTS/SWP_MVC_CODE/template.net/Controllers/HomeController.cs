using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Repositories;
using System.Diagnostics;
using testtemplate.Models;
using SWP_Management.Repo.Entities;
using System.ComponentModel.DataAnnotations;
using Microsoft.Identity.Client;
using System.Net;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

namespace testtemplate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISemesterRepository _semesterRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IAssignmentStudentRepository _assignmentStudentRepository;
        private readonly IStudentRepository _studentRepository;


        public HomeController(ISemesterRepository semesterRepository,
                                ICourseRepository courseRepository, 
                                IAssignmentRepository assignmentRepository,
                                IAssignmentStudentRepository assignmentStudentRepository,
                                IStudentRepository studentRepository)
        {
            _semesterRepository = semesterRepository;
            _courseRepository = courseRepository;
            _assignmentRepository = assignmentRepository;
            _assignmentStudentRepository = assignmentStudentRepository;
            _studentRepository = studentRepository;
        }



        public IActionResult Index()
        {
            var student = _studentRepository.GetById(ReadCookie());
            if(student == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var semesterList = _semesterRepository.GetList().ToList();
            ViewData["SemesterList"] = semesterList;

            var courseList = _courseRepository.GetList().ToList();
            ViewData["CourseList"] = courseList;

            return View();
        }

        public IActionResult Dashboard()
        {
          
            return View();
        }

        public IActionResult GetTask()
        {
            string studentId = ReadCookie();
           
            GetTaskList(studentId);

            return View();
        }

        public void GetTaskList(string studentId)
        {
            var assignmentList = _assignmentStudentRepository.GetList().Where(p => p.StudentId.Equals(studentId)).ToList();
           
            var taskList = _assignmentRepository.GetList().ToList();
            List<Assignment> tasks = new List<Assignment>();
            for (int i = 0; i < assignmentList.Count; i++)
            {
                for (int j = 0; j < taskList.Count; j++)
                {
                    if (assignmentList[i].TaskId.Equals(taskList[j].Id))
                    {
                        tasks.Add(taskList[j]);
                    }
                }
            }
            ViewData["AssignmentStudent"] = assignmentList;
            ViewData["TaskList"] = tasks;
        }

        public async Task<IActionResult> AddTask()
        {
            ReadCookie();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTask(Assignment assignment)
        {
            string studentId = ReadCookie();

            var student = _studentRepository.GetById(studentId);


            // add
            var existing = _assignmentRepository.GetById(assignment.Id);
            if (existing != null)
            {
                ViewBag.Result = "Duplicate";
                return View();
            }

            AssignmentStudent assignmentStudent = new AssignmentStudent();
            assignmentStudent.Status = "Ongoing";
            assignmentStudent.StudentId = student.Id;
            assignmentStudent.TaskId = assignment.Id;
            assignmentStudent.Student = student;
            assignmentStudent.Task = assignment;

            
            _assignmentRepository.Add(assignment);

            _assignmentStudentRepository.Add(assignmentStudent);
            
            return RedirectToAction("GetTask");
        }
                
        public async Task<IActionResult> UpdateTask(string id)
        {
            string studentId = ReadCookie();
            var assignmentList = _assignmentStudentRepository.GetList().Where(p => p.StudentId.Equals(studentId)).ToList();
            ViewData["AssignmentStudent"] = assignmentList;
            var currentAssignment = _assignmentRepository.GetById(id);
            return View(currentAssignment);
        }

        

        [HttpPost]
        public async Task<IActionResult> UpdateTask(string TaskId, string Name, string Descripton, DateTime DateStart, DateTime DateEnd, int AssignmentStudentId, string Status)
        {
            string studentId = ReadCookie();
            var student = _studentRepository.GetById(studentId);
            var assignment = _assignmentRepository.GetById(TaskId);
            AssignmentStudent assignmentStudent = new AssignmentStudent();
            assignmentStudent = _assignmentStudentRepository.GetById(AssignmentStudentId);

            assignmentStudent.StudentId = studentId;
            assignmentStudent.Status = Status;
            assignmentStudent.TaskId = assignment.Id;
            assignmentStudent.Student = student;
            assignmentStudent.Task = assignment;



            _assignmentStudentRepository.Update(assignmentStudent);


            _assignmentRepository.Update(assignment);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTask(string id)
        {
            _assignmentRepository.Delete(id);
            return RedirectToAction("GetTask");
        }

        public async Task<IActionResult> Logout()
        {
            RemoveCookie();
            return RedirectToAction("Index");
        }


        //public void CreateCookie()
        //{
        //    String key = "User";
        //    String value = "SE173508";
        //    CookieOptions cook = new CookieOptions();
        //    {
        //        cook.Expires = DateTime.Now.AddDays(1);
        //    };
        //    Response.Cookies.Append(key, value, cook);

        //}
        
        public string ReadCookie()
        {
            String key = "User";
            var cookieValue = Request.Cookies[key];
            ViewData["cookieValue"] = cookieValue;
            return cookieValue;

        }
        public void RemoveCookie()
        {
            String key = "User";
            String value = string.Empty;
            CookieOptions cook = new CookieOptions();
            {
                cook.Expires = DateTime.Now.AddDays(-1);
            };
            Response.Cookies.Append(key, value, cook);

        }

    }

}