using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using SWP_Management.API.Controllers;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;

namespace SWP_Frontend_Admin.Controllers
{ 
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IActionResult> Index()
        {
            List<Student> students = new List<Student>();
            var response = _studentRepository.GetList().ToJson();
            students = JsonConvert.DeserializeObject<List<Student>>(response);

            ViewData["StudentList"] = students;
            return View(students);
        }

        public ViewResult AddStudent() => View();

        [HttpPost]
        public async Task<IActionResult> AddStudent(Student Student)
        {
            var student = _studentRepository.GetById(Student.Id);

            //Validation
            bool returnSwitch = false;
            if (!new Validator().validate(Student.Id, @"^SE\d{6}$")){
                ViewData["Invalid"] = "Invalid";
                returnSwitch = true;
            }

            if(Student.Id.Length > 50)
            {
                ViewData["IdLength"] = "IdLength";
                returnSwitch = true;
            }

            if(Student.Name.Length > 200)
            {
                ViewData["NameLength"] = "NameLength";
                returnSwitch = true;
            }

            if (Student.Main.Length > 200)
            {
                ViewData["MainLength"] = "MainLength";
                returnSwitch = true;
            }

            if (returnSwitch) return View(Student);

            //Check for Duplication
            if (student != null)
            {
                ViewBag.Result = "Duplicate";
                return View(Student);
            }
            _studentRepository.Add(Student);           
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> UpdateStudent(string id)
        {
            Student Student = new Student();
            Student = _studentRepository.GetById(id);
            ViewData["Student"] = Student;
            return View(Student);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateStudent(string id, Student Student)
        {
            _studentRepository.Update(Student);
            ViewBag.Result = "Success";
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            _studentRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }   
}
