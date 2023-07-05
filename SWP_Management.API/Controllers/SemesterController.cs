using Microsoft.AspNetCore.Mvc;
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



        // Add
        // Update
        // Delete

    }
}
