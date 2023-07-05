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



        // Add
        // Update
        // Delete
    }
}
