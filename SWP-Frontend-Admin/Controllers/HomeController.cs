using Microsoft.AspNetCore.Mvc;

namespace SWP_Frontend_Admin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
