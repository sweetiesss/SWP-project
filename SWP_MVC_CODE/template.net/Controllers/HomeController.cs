using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Repositories;
using System.Diagnostics;
using testtemplate.Models;
using SWP_Management.Repo.Entities;
using System.ComponentModel.DataAnnotations;
using Microsoft.Identity.Client;
using System.Net;
using Azure.Core;
using Microsoft.AspNetCore.Http;

namespace testtemplate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISemesterRepository _semesterRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IAssignmentRepository _assignmentRepository;


        public HomeController(ISemesterRepository semesterRepository,
                                ICourseRepository courseRepository, IAssignmentRepository assignmentRepository)
        {
            _semesterRepository = semesterRepository;
            _courseRepository = courseRepository;
            _assignmentRepository = assignmentRepository;
        }

        //public static void SetCookie(HttpContext context, string key, string value, int expireDay = 1)
        //{
        //    Cookie cookie = new Cookie();
        //    cookie.Name = "User";
        //    cookie.Value = "SE173508";
        //    cookie.Expires = DateTime.Now.AddDays(expireDay);
        //    .Add(cookie);
        //}

        public IActionResult Index()
        {
            var semesterList = _semesterRepository.GetList().ToList();
            ViewData["SemesterList"] = semesterList;

            var courseList = _courseRepository.GetList().ToList();
            ViewData["CourseList"] = courseList;

            return View();
        }

        public IActionResult Dashboard(string CourseId)
        {
            if (CourseId != null)
            {
                ViewData["CourseId"] = CourseId;
            }

            return View();
        }

        public IActionResult GetTask(string Name)
        {
            //if (Name != null)
            //{
            //    var task = _assignmentRepository.GetList().Where(p => p.Name.Contains(Name)).FirstOrDefault();
            //    return View(task);
            //}

            var taskList = _assignmentRepository.GetList().ToList();
            return View(taskList);
        }

        public ViewResult AddTask() => View();

        [HttpPost]
        public async Task<IActionResult> AddTask(Assignment assignment)
        {


            // add
            var existing = _assignmentRepository.GetById(assignment.Id);
            if (existing != null)
            {
                ViewBag.Result = "Duplicate";
                return View();
            }
            _assignmentRepository.Add(assignment);
            return RedirectToAction("GetTask");
        }

        public async Task<IActionResult> UpdateTask(string id)
        {
            var currentAssignment = _assignmentRepository.GetById(id);
            return View(currentAssignment);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTask(string id, Assignment assignment)
        {
            var updateAssignment = _assignmentRepository.GetById(id);
            if (updateAssignment != null)
            {
                updateAssignment = assignment;
            }

            _assignmentRepository.Update(updateAssignment);
            return RedirectToAction("GetTask");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTask(string id)
        {
            _assignmentRepository.Delete(id);
            return RedirectToAction("GetTask");
        }
        //Cookie
        public IActionResult CreateCookie()
        {
            String key = "User";
            String value = "SE173508";
            CookieOptions cook = new CookieOptions();
            {
                cook.Expires = DateTime.Now.AddDays(1);
            };
            Response.Cookies.Append(key, value, cook);
            return View("Test");
        }
        public IActionResult ReadCookie()
        {
            String key = "User";
           var cookieValue = Request.Cookies[key];
            ViewData["cookieValue"] = cookieValue;
            return View("Test");
        }
        public IActionResult RemoveCookie()
        {
            String key = "User";
            String value = string.Empty;
            CookieOptions cook = new CookieOptions();
            {
                cook.Expires = DateTime.Now.AddDays(-1);
            };
            Response.Cookies.Append(key, value, cook);
            return View("Test");
        }
    }

}