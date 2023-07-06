using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;

namespace SWP_Management.API.Controllers
{
    public class LecturerController : Controller
    {
        private readonly ILecturerRepository _lecturerRepository;

        public LecturerController(ILecturerRepository lecturerRepository)
        {
            _lecturerRepository = lecturerRepository;
        }

        public IActionResult Index()
        {
            var lecturerList = _lecturerRepository.GetList();
            return View(lecturerList);
        }


        public ViewResult AddLecturer() => View();
        // Add
        [HttpPost]
        public async Task<IActionResult> AddLecturer(Lecturer Lecturer)
        {
            var lecturer = _lecturerRepository.GetById(Lecturer.Id);
            if (lecturer != null)
            {
                ViewBag.Result = "Duplicate";
                return View(Lecturer);
            }
            _lecturerRepository.Add(Lecturer);
            return RedirectToAction("Index");
        }
        // Update

        public async Task<IActionResult> UpdateLecturer(string id)
        {
            Lecturer Lecturer = new Lecturer();
            Lecturer = _lecturerRepository.GetById(id);
            return View(Lecturer);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateLecturer(string id, Lecturer Lecturer)
        {
            _lecturerRepository.Update(Lecturer);
            ViewBag.Result = "Success";
            return RedirectToAction("Index");
        }
        // Delete

        [HttpPost]
        public async Task<IActionResult> DeleteLecturer(string id)
        {
            _lecturerRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
