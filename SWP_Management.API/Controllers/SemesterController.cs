using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;

namespace SWP_Management.API.Controllers
{
    public class SemesterController : Controller
    {
        private readonly ISemesterRepository _semesterRepository;

        public SemesterController(ISemesterRepository semesterRepository)
        {
            _semesterRepository = semesterRepository;
        }


        public IActionResult Index()
        {
            var semesterList = _semesterRepository.GetList();
            return View(semesterList);
        }


        public ViewResult AddSemester() => View();
        // Add

        [HttpPost]
        public async Task<IActionResult> AddSemester(Semester semester)
        {
            var existingSemester = _semesterRepository.GetList().Where(p => p.Id.Equals(semester.Id)).FirstOrDefault();
            if (existingSemester != null)
            {
                ViewBag.Result = "Duplicate";
                return View();
            }
            _semesterRepository.Add(semester);
            return RedirectToAction("Index");
        }



        // Update
        public async Task<IActionResult> UpdateSemester(string id)
        {
            var semester = _semesterRepository.GetById(id);
            return View(semester);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSemester(string id, string name)
        {
            var semester = _semesterRepository.GetById(id);
            semester.Name = name;
            _semesterRepository.Update(semester);
            return RedirectToAction("Index");

        }

        // Delete
        [HttpPost]
        public async Task<IActionResult> DeleteSemester(string id)
        {
            _semesterRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }



}
