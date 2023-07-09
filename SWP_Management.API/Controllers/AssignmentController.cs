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

        public async Task<IActionResult> Index(string Name)
        {

            if (Name == null) Name = string.Empty;


            var AssignmentList = _assignmentRepository.GetList();
            List<Assignment> assignments = new List<Assignment>();
            foreach (Assignment asm in AssignmentList)
            {
                if (asm.Name.Contains(Name)) assignments.Add(asm);
            }

            ViewData["AssignList"] = assignments;
            return View(assignments);
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
            var existing = _assignmentRepository.GetById(Assignment.Id);
            if(existing != null)
            {
                ViewBag.Result = "Duplicate";
                return View();
            }
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
