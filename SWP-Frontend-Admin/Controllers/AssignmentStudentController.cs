using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SWP_Frontend_Admin.Models;

namespace SWP_Frontend_Admin.Controllers
{
    public class AssignmentStudentController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<AssignmentStudent> assignmentStudents = new List<AssignmentStudent>();
            using (var httpClient = new HttpClient())
            {
                using (var response= await httpClient.GetAsync("https://localhost:7219/api/assignmentStudents"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    assignmentStudents = JsonConvert.DeserializeObject<List<AssignmentStudent>>(apiResponse);
                }

            }
            return View(assignmentStudents);
        }
    }
}
