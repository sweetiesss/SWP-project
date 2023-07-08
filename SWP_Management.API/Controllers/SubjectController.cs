using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using SWP_Frontend_Admin.Pages.AssignmentStudent;
using SWP_Management.Repo.Entities;
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


        public ViewResult AddSubject() => View();
        // Add

        [HttpPost]
        public async Task<IActionResult> AddSubject(string id, string name)
        {

            // Validation
            bool returnSwitch = false;

            if (!new Validator().validate(id, @"^[a-zA-Z]{3}\d{3}[c]?$"))
            {
                ViewData["Invalid"] = "Invalid";
                returnSwitch = true;
            }

            if (id.Length > 50)
            {
                ViewData["IdLength"] = "IdLength";
                returnSwitch = true;
            }

            if (name.Length > 200)
            {
                ViewData["NameLength"] = "NameLength";
                returnSwitch = true;
            }

            if (returnSwitch) return View();

            // Check for Duplicate
            var existingSubject = _subjectRepository.GetById(id);
            if (existingSubject != null)
            {
                ViewBag.Result = "Duplicate";
                return View();
            }
            Subject subject = new Subject();
            subject.Name = name;
            subject.Id = id;
            _subjectRepository.Add(subject);
            return RedirectToAction("Index");
        }
        // Update

        public async Task<IActionResult> UpdateSubject(string id)
        {
            var subject = _subjectRepository.GetById(id);
            return View(subject);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSubject(Subject Subject)
        {

            //Validation
            bool returnSwitch = false;
            if (Subject.Name.Length > 200)
            {
                ViewData["NameLength"] = "NameLength";
                returnSwitch = true;
            }
            if (returnSwitch) return View();



            var subject = _subjectRepository.GetById(Subject.Id);
            
            subject.Name = Subject.Name;
            
            _subjectRepository.Update(subject);
            return RedirectToAction("Index");
        }
        // Delete

        [HttpPost]
        public async Task<IActionResult> DeleteSubject(string id)
        {
            _subjectRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
