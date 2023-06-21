using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SWP_Frontend_Admin.Models;
using System.Text;

namespace SWP_Frontend_Admin.Controllers
{
    public class StudentController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Student> students = new List<Student>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7219/api/students"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    students = JsonConvert.DeserializeObject<List<Student>>(apiResponse);
                }
            }

            return View(students);
        }

        //public ViewResult AddAssignment() => View();

        //[HttpPost]
        //public async Task<IActionResult> AddAssignment(Assignment Assignment)
        //{
        //    Assignment receivedAssignment = new Assignment();
        //    using (var httpClient = new HttpClient())
        //    {
        //        StringContent content = new StringContent(JsonConvert.SerializeObject(Assignment), Encoding.UTF8, "application/json");

        //        using (var response = await httpClient.PostAsync("https://localhost:7219/api/assignments", content))
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();
        //            receivedAssignment = JsonConvert.DeserializeObject<Assignment>(apiResponse);
        //        }
        //    }
        //    return View(receivedAssignment);
        //}

        public ViewResult AddStudent() => View();

        [HttpPost]
        public async Task<IActionResult> AddStudent(Student Student)
        {
            Student receivedStudent = new Student();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(Student), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:7219/api/students", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedStudent =  JsonConvert.DeserializeObject<Student>(apiResponse);
                }
            }
            return View(receivedStudent);
        }


        public async Task<IActionResult> UpdateStudent(string id)
        {
            Student Student = new Student();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7219/api/students" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Student = JsonConvert.DeserializeObject<Student>(apiResponse);
                }
            }
            return View(Student);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateStudent(string id, Student Student)
        {
            Student receivedStudent = new Student();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(Student), Encoding.UTF8, "application/json");

                String url = "https://localhost:7219/api/students/" + Student.Id;


                using (var response = await httpClient.PutAsync(url, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedStudent = JsonConvert.DeserializeObject<Student>(apiResponse);
                }
            }

            return View(receivedStudent);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:7219/api/students/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index");
        }
    }   
}
