using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;
using System.Text;

namespace SWP_Frontend_Admin.Controllers
{

    public class AssignmentController : Controller
    {

        private readonly IAssignmentRepository _assignmentRepository;

        public AssignmentController(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }

        public async Task<IActionResult> Index()
        {
            List<Assignment> AssignmentList = new List<Assignment>();

            var response = _assignmentRepository.GetList().ToJson();
            AssignmentList = JsonConvert.DeserializeObject<List<Assignment>>(response);

            ViewData["AssignList"] = AssignmentList;
            return View(AssignmentList);
        }

        public ViewResult AddAssignment() => View();

        [HttpPost]
        public async Task<IActionResult> AddAssignment(Assignment Assignment)
        {
            _assignmentRepository.Add(Assignment);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateAssignment(string id)
        { 
            Assignment Assignment = new Assignment();
            Assignment = _assignmentRepository.GetById(id);
			return View(Assignment);
		}


        [HttpPost]
        public async Task<IActionResult> UpdateAssignment(string id, Assignment Assignment)
        {
            _assignmentRepository.Update(Assignment);
            ViewBag.Result = "Success";
			return RedirectToAction("Index");
        }


		[HttpPost]
		public async Task<IActionResult> DeleteAssignment(string id)
		{
            _assignmentRepository.Delete(id);
			return RedirectToAction("Index");
		}



	}
}
