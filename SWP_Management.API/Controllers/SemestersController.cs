using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SWP_Management.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class semestersController : ControllerBase
    {
        private readonly ISemesterRepository _semesterRepository;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public semestersController(ISemesterRepository semesterRepository)
        {
            _semesterRepository = semesterRepository;

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
        }

        [HttpGet]
        public IActionResult GetList()
        {
            var semesters = _semesterRepository.GetList();
            var json = JsonSerializer.Serialize(semesters, _jsonSerializerOptions);
            return Content(json, "application/json");
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var semester = _semesterRepository.GetById(id);
            if (semester == null)
                return NotFound();

            var json = JsonSerializer.Serialize(semester, _jsonSerializerOptions);
            return Content(json, "application/json");
        }

        [HttpPost]
        public IActionResult Add(Semester newSemester)
        {
            _semesterRepository.Add(newSemester);
            _semesterRepository.SaveChanges();
            var json = JsonSerializer.Serialize(newSemester, _jsonSerializerOptions);
            return CreatedAtAction(nameof(GetById), new { id = newSemester.Id }, json);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, Semester updatedSemester)
        {
            var existingSemester = _semesterRepository.GetById(id);
            if (existingSemester == null)
                return NotFound();

            existingSemester.Name = updatedSemester.Name;
            _semesterRepository.Update(existingSemester);
            _semesterRepository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var semester = _semesterRepository.GetById(id);
            if (semester == null)
                return NotFound();

            _semesterRepository.Delete(semester);
            _semesterRepository.SaveChanges();

            return NoContent();
        }
    }
}
