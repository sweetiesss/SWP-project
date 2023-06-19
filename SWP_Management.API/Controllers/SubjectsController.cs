using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SWP_Management.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class subjectController : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public subjectController(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
        }

        [HttpGet]
        public IActionResult GetSubjects()
        {
            var subjects = _subjectRepository.GetList();
            var json = JsonSerializer.Serialize(subjects, _jsonSerializerOptions);
            return Content(json, "application/json");
        }

        [HttpGet("{id}")]
        public IActionResult GetSubjectById(string id)
        {
            var subject = _subjectRepository.GetById(id);
            if (subject == null)
            {
                return NotFound();
            }
            var json = JsonSerializer.Serialize(subject, _jsonSerializerOptions);
            return Content(json, "application/json");
        }

        [HttpPost]
        public IActionResult AddSubject(Subject newSubject) // Modified parameter name
        {
            _subjectRepository.Add(newSubject); // Use the modified parameter name
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSubject(string id, Subject updatedSubject) // Modified parameter name
        {
            var existingSubject = _subjectRepository.GetById(id);
            if (existingSubject == null)
            {
                return NotFound();
            }
            existingSubject.Name = updatedSubject.Name; // Update other properties as needed
            _subjectRepository.Update(existingSubject);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSubject(string id)
        {
            var existingSubject = _subjectRepository.GetById(id);
            if (existingSubject == null)
            {
                return NotFound();
            }
            _subjectRepository.Delete(id);
            return Ok();
        }
    }
}
