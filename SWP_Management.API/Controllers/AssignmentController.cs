using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using SWP_Management.API.Controllers;
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

        // Add
        [HttpPost]
        public async Task<IActionResult> AddAssignment(Assignment Assignment)
        {
            //Validation
            bool returnSwitch = false;

            if (!new Validator().validate(Assignment.Id, @"^Task\d*$"))
            {
                ViewData["Id"] = "Invalid";
                returnSwitch = true;
            }

            if(Assignment.Id.Length > 50)
            {
                ViewData["IdLength"] = "IdLength";
                returnSwitch = true;
            }

            if (Assignment.DateStart.Date > Assignment.DateEnd.Date)
            {
                ViewData["DateTime"] = "DateTime";
                returnSwitch = true;
            }

            if (Assignment.Name.Length > 200)
            {
                ViewData["NameLength"] = "NameLength";
                returnSwitch = true;
            }

            if (Assignment.Description.Length > 200)
            {
                ViewData["DescriptionLength"] = "DescriptionLength";
                returnSwitch = true;
            }

            if (returnSwitch) return View(Assignment);

            // add
            _assignmentRepository.Add(Assignment);
            return RedirectToAction("Index");
        }

        // Update
        public async Task<IActionResult> UpdateAssignment(string id)
        { 
            Assignment Assignment = new Assignment();
            Assignment = _assignmentRepository.GetById(id);
			return View(Assignment);
		}


        [HttpPost]
        public async Task<IActionResult> UpdateAssignment(string id, Assignment Assignment)
        {
            //Validation
            bool returnSwitch = false;
            if (Assignment.DateStart.Date > Assignment.DateEnd.Date)
            {
                ViewData["DateTime"] = "DateTime";
                returnSwitch = true;
            }

            if (Assignment.Name.Length > 200)
            {
                ViewData["NameLength"] = "NameLength";
                returnSwitch = true;
            }

            if (Assignment.Description.Length > 200)
            {
                ViewData["DescriptionLength"] = "DescriptionLength";
                returnSwitch = true;
            }

            if (returnSwitch) return View(Assignment);

            // update
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
