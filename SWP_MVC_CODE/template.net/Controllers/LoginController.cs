using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Repositories;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace testtemplate.Controllers
{
    public class LoginController : Controller
    {
        static bool first = true;
        private readonly IAccountRepository _accountRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ILecturerRepository _lecturerRepository;

        public LoginController(IAccountRepository accountRepository, IStudentRepository studentRepository, ILecturerRepository lecturerRepository)
        {
            _accountRepository = accountRepository;
            _studentRepository = studentRepository;
            _lecturerRepository = lecturerRepository;
        }

        public IActionResult Index()
        {

            if (ReadCookie() == null)
            {
                return View();
            }

            var student = _studentRepository.GetById(ReadCookie());
            var lecturer = _lecturerRepository.GetById(ReadCookie());
            if (student != null)
            {
                first = false;
                return RedirectToAction("Index", "Home");
            }
            if (lecturer != null)
            {
                first = false;
                return RedirectToAction("Index", "Lecturer");
            }
            if (first) first = false;
            else ViewData["Failed"] = "Failed";
            return View();
        }

        public IActionResult Login(string Username, string Password)
        {
            var account = _accountRepository.GetList().Where(p => p.Username.Equals(Username)
                                                                && p.Password.Equals(Password)).FirstOrDefault();
            if (account != null)
            {
                if (account.StudentId != null)
                {
                    CreateCookie(account.StudentId);
                    return RedirectToAction("Index", "Home");
                }
                if (account.TeacherId != null)
                {
                    CreateCookie(account.TeacherId);
                    return RedirectToAction("Index", "Lecturer");
                }
            }
            CreateCookie("LoginFailed");
            return RedirectToAction("Index");
        }


        public void CreateCookie(string id)
        {
            String key = "User";
            String value = id;
            CookieOptions cook = new CookieOptions();
            {
                cook.Expires = DateTime.Now.AddDays(1);
            };
            Response.Cookies.Append(key, value, cook);

        }

        public string ReadCookie()
        {
            String key = "User";
            var cookieValue = Request.Cookies[key];
            ViewData["cookieValue"] = cookieValue;
            return cookieValue;

        }
    }
}
