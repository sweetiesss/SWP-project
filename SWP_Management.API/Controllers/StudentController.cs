using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;
using System.Collections.Generic;
using System.Text;

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
            if(student != null)
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
