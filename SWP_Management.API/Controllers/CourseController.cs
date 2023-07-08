using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;

namespace SWP_Management.API.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ISemesterRepository _semesterRepository;
        private readonly ILecturerRepository _lecturerRepository;

        public CourseController(ICourseRepository courseRepository,
                                 ISubjectRepository subjectRepository,
                                 ISemesterRepository semesterRepository,
                                 ILecturerRepository lecturerRepository)
        {
            _courseRepository = courseRepository;
            _subjectRepository = subjectRepository;
            _semesterRepository = semesterRepository;
            _lecturerRepository = lecturerRepository;
        }
        public IActionResult Index()
        {
            var courseList = _courseRepository.GetList().ToList();
            return View(courseList);
        }


        public async Task<IActionResult> AddCourse()
        {
            var SemesterList = _semesterRepository.GetList();
            ViewData["SemesterList"] = SemesterList;

            var SubjectList = _subjectRepository.GetList();
            ViewData["SubjectList"] = SubjectList;

            var LecturerList = _lecturerRepository.GetList();
            ViewData["LecturerList"] = LecturerList;
            return View();
        }

        // Add
        [HttpPost]
        public async Task<IActionResult> AddCourse(string SemesterId, string SubjectId, string LecturerId,
                                                   string Id, string name, DateTime dateStart, DateTime dateEnd)
        {
            AddCourse();
            // Validation
            bool returnSwitch = false;
            if (Id.Length > 50)
            {
                ViewData["IdLength"] = "IdLength";
                returnSwitch = true;
            }

            if (!new Validator().validate(Id, @"^[C]\d*$"))
            {
                ViewData["Invalid"] = "Invalid";
                returnSwitch = true;
            }

            if (name.Length > 200)
            {
                ViewData["NameLength"] = "NameLength";
                returnSwitch = true;
            }

            if(dateStart.Date > dateEnd.Date)
            {
                ViewData["DateTime"] = "DateTime";
                returnSwitch = true;
            }

            if (returnSwitch) return View();

            // Check for Duplicate
            Semester semester = new Semester();
            Subject subject = new Subject();
            Lecturer lecturer = new Lecturer();
            Course course = new Course();

            var existingCourse = _courseRepository.GetById(Id);
            if (existingCourse != null)
            {
                ViewBag.Result = "Duplicate Course";
                return View();
            }
         
            semester = _semesterRepository.GetById(SemesterId);
            subject = _subjectRepository.GetById(SubjectId);
            lecturer = _lecturerRepository.GetById(LecturerId);
            // Insert data to an entity for add
            course.Id = Id;
            course.Name = name;
            course.DateStart = dateStart;
            course.DateEnd = dateEnd;
            course.SubjectId = SubjectId;
            course.SemesterId = SemesterId;
            course.LecturerId = LecturerId;
            course.Subject = subject;
            course.Semester = semester;
            course.Lecturer = lecturer;

            // add
            _courseRepository.Add(course);


            return RedirectToAction("Index");
        }
        
        //Get course when press update
        public async Task<IActionResult> UpdateCourse(string Id)
        {

            var SemesterList = _semesterRepository.GetList();
            ViewData["SemesterList"] = SemesterList;


            var SubjectList = _subjectRepository.GetList();
            ViewData["SubjectList"] = SubjectList;

            var LecturerList = _lecturerRepository.GetList();
            ViewData["LecturerList"] = LecturerList;

            var course = _courseRepository.GetById(Id);
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCourse(string SemesterId, string SubjectId, string LecturerId,
                                                   string Id, string name, DateTime dateStart, DateTime dateEnd)
        {
            UpdateCourse(Id);
            //Validation
            bool returnSwitch = false;

            if (name.Length > 200)
            {
                ViewData["NameLength"] = "NameLength";
                returnSwitch = true;
            }

            if (dateStart.Date > dateEnd.Date)
            {
                ViewData["DateTime"] = "DateTime";
                returnSwitch = true;
            }
            if (returnSwitch) return View(_courseRepository.GetById(Id));

            Semester semester = new Semester();
            Subject subject = new Subject();
            Lecturer lecturer = new Lecturer();

            var existingCourse = _courseRepository.GetById(Id);
            if (existingCourse != null)
            {
                semester = _semesterRepository.GetById(SemesterId);
                subject = _subjectRepository.GetById(SubjectId);
                lecturer = _lecturerRepository.GetById(LecturerId);
                // Insert data to an entity for add
                existingCourse.Name = name;
                existingCourse.DateStart = dateStart;
                existingCourse.DateEnd = dateEnd;
                existingCourse.SubjectId = SubjectId;
                existingCourse.SemesterId = SemesterId;
                existingCourse.LecturerId = LecturerId;
                existingCourse.Subject = subject;
                existingCourse.Semester = semester;
                existingCourse.Lecturer = lecturer;

                _courseRepository.Update(existingCourse);
                return RedirectToAction("Index");
            }
            UpdateCourse(Id);
            ViewBag.Result = "Null";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCourse(string Id)
        {
            var course = _courseRepository.GetById(Id);
            if (course != null)
            {
                _courseRepository.Delete(Id);
            }
            return RedirectToAction("Index");
        }
    }
}
