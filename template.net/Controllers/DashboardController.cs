using Microsoft.AspNetCore.Mvc;

namespace testtemplate.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
