using Microsoft.AspNetCore.Mvc;

namespace testtemplate.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
