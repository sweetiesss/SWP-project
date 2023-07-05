using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Repositories;

namespace SWP_Management.API.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectController(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }


        public IActionResult Index()
        {
            var subjectList = _subjectRepository.GetList();
            return View(subjectList);
        }



        // Add
        // Update
        // Delete
    }
}
