using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SWP_Frontend_Admin.Models;

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
    }
}
