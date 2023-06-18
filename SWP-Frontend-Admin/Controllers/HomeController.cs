using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SWP_Frontend_Admin.Models;
using System.Text;

namespace SWP_Frontend_Admin.Controllers
{
    public class HomeController : Controller
    {
        
        public async Task<IActionResult> Index()
        {
            List<Assignment> AssignmentList = new List<Assignment>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7219/api/assignments"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
					AssignmentList = JsonConvert.DeserializeObject<List<Assignment>>(apiResponse);
                }
            }
            ViewData["AssignList"] = AssignmentList;
            return View(AssignmentList);
        }

        public ViewResult AddAssignment() => View();

        [HttpPost]
        public async Task<IActionResult> AddAssignment(Assignment Assignment)
        {
            Assignment receivedAssignment = new Assignment();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(Assignment), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:7219/api/assignments", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedAssignment = JsonConvert.DeserializeObject<Assignment>(apiResponse);
                }
            }
            return View(receivedAssignment);
        }

    }
}
