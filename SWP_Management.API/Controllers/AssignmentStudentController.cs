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

        public async Task<IActionResult> AddAssignmentStudent()                 //Get two list and add it to viewdata
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


        [HttpPost]
        public async Task<IActionResult> SelectAssignmentStudent(String taskId, String studentId)
        {
            Student student = new Student();
            AssignmentStudent assignmentStudent = new AssignmentStudent();
            Assignment assignment = new Assignment();

            assignment = _assignmentRepository.GetById(taskId);
            student = _studentRepository.GetById(studentId);
            //using (var httpClient = new HttpClient())
            //{
            //    //Get assignment with taskId from dropdown list and assign it to an Assignment object
            //    using (var response = await httpClient.GetAsync("https://localhost:7219/api/assignments/" + taskId))
            //    {
            //        string apiResponse = await response.Content.ReadAsStringAsync();
            //        assignment = JsonConvert.DeserializeObject<Assignment>(apiResponse);
            //    }
            //    ViewData["Assignment"] = assignment;

            //    //Get student with studentId from dropdown list and assign it to a Student object
            //    using (var response = await httpClient.GetAsync("https://localhost:7219/api/students/" + studentId))
            //    {
            //        string apiResponse = await response.Content.ReadAsStringAsync();
            //        student = JsonConvert.DeserializeObject<Student>(apiResponse);
            //    }
            //    ViewData["Student"] = student;
                //Assign everything to AssignmentStudent
                assignmentStudent.Task = assignment;
                assignmentStudent.Student = student;
                assignmentStudent.TaskId = taskId;
                assignmentStudent.StudentId = studentId;
            //Create a JSON to call POST API to add to database
            //StringContent content = new StringContent(JsonConvert.SerializeObject(assignmentStudent), Encoding.UTF8, "application/json");
            //string JSONChecker = await content.ReadAsStringAsync();
            //ViewData["JSONCheck"] = JSONChecker;
            //using (var response = await httpClient.PostAsync("https://localhost:7219/api/assignmentStudents", content))
            //    {
            //        string apiResponse = await response.Content.ReadAsStringAsync();

            //}
            _assignmentStudentRepository.Add(assignmentStudent);
            

            return View();
        }



        public async Task<IActionResult> UpdateStudentAssignment(string taskId, string studentId)
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
            assignmentStudent = _assignmentStudentRepository.GetList().Where(p => p.TaskId.Equals(taskId) &&
                                                                                  p.StudentId.Equals(studentId)).FirstOrDefault();

            ViewData["AssignmentStudent"] = assignmentStudent;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAssStudent(string taskId, string studentId)
        {
            Student student = new Student();
            AssignmentStudent assignmentStudent = new AssignmentStudent();
            Assignment assignment = new Assignment();

            assignment = _assignmentRepository.GetList().Where(p => p.Id.Equals(taskId)).FirstOrDefault();
            student = _studentRepository.GetById(studentId);

            assignmentStudent.Task = assignment;
            assignmentStudent.Student = student;
            assignmentStudent.TaskId = taskId;
            assignmentStudent.StudentId=studentId;
            _assignmentStudentRepository.Update(assignmentStudent);
            ViewBag.Result = "Success";
            return View(assignmentStudent);
            return RedirectToAction("Index");
        }


		[HttpPost]
		public async Task<IActionResult> DeleteAssignmentStudent(string taskId, string studentId)
		{
            AssignmentStudent assignmentStudent = new AssignmentStudent();
            assignmentStudent = _assignmentStudentRepository.GetList().Where(p => p.TaskId.Equals(taskId) &&
                                                                                  p.StudentId.Equals(studentId)).FirstOrDefault();
            _assignmentStudentRepository.Delete(assignmentStudent.Id);
			return RedirectToAction("Index");
		}

		//[HttpPost]
		//public async Task<IActionResult> SelectAssignmentStudent(String taskId, String studentId)
		//{
		//    AssignmentStudent assignmentStudent = new AssignmentStudent();
		//    Assignment assignment = new Assignment();
		//    using (var httpClient = new HttpClient())
		//    {
		//        using (var response = await httpClient.GetAsync("https://localhost:7219/api/assignments/" + taskId))
		//        {
		//            string apiResponse = await response.Content.ReadAsStringAsync();
		//            assignment = JsonConvert.DeserializeObject<Assignment>(apiResponse);
		//        }
		//        ViewData["Assignment"] = assignment;
		//        assignmentStudent.Task = assignment;
		//    }
		//    Student student = new Student();
		//    using (var httpClient = new HttpClient())
		//    {
		//        using (var response = await httpClient.GetAsync("https://localhost:7219/api/students/" + studentId))
		//        {
		//            string apiResponse = await response.Content.ReadAsStringAsync();
		//            student = JsonConvert.DeserializeObject<Student>(apiResponse);
		//        }
		//        ViewData["Student"] = student;
		//        assignmentStudent.Student = student;
		//        assignmentStudent.TaskId = taskId;
		//        assignmentStudent.StudentId = studentId;
		//    }
		//        using (var httpClient = new HttpClient())
		//        {
		//            StringContent content = new StringContent(JsonConvert.SerializeObject(assignmentStudent), Encoding.UTF8, "application/json");
		//            string JSONChecker = await content.ReadAsStringAsync();
		//                ViewData["JSONCheck"] = JSONChecker;
		//        //using (var response = await httpClient.PostAsync("https://localhost:7219/api/assignmentStudents", content))
		//        //    {
		//        //        string apiResponse = await response.Content.ReadAsStringAsync();

		//        //}
		//        }

		//    return View();
		//}



	}
        }
    
