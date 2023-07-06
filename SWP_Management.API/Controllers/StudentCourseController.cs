using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;
namespace SWP_Management.API.Controllers
{
    public class StudentCourseController : Controller
    { 

        private readonly IStudentCourseRepository _studentCourseRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;

        public StudentCourseController(IStudentCourseRepository studentCourseRepository,
                                        ICourseRepository courseRepository,
                                        IStudentRepository studentRepository)
        {
            _studentCourseRepository = studentCourseRepository;
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
        }

        // Render Index Page
        public IActionResult Index()
        {
            var studentCourseList = _studentCourseRepository.GetList();
            return View(studentCourseList);
        }

        // Add List of Course and Student to View
        public async Task<IActionResult> AddStudentCourse()
        {
            var courseList = _courseRepository.GetList();
            ViewData["CourseList"] = courseList;


            var studentList = _studentRepository.GetList();
            ViewData["StudentList"] = studentList;

            return View();
        }


        public ViewResult SelectStudentCourse() => View();

        // Add
        [HttpPost]
        public async Task<IActionResult> SelectStudentCourse(String CourseId, String studentId)
        {
            Student student = new Student();
            StudentCourse studentCourse = new StudentCourse();
            Course course = new Course();

            course = _courseRepository.GetById(CourseId);
            student = _studentRepository.GetById(studentId);

            // Insert data to an entity for add
            studentCourse.Course = course;
            studentCourse.Student = student;
            studentCourse.CourseId = CourseId;
            studentCourse.StudentId = studentId;

            // add
            _studentCourseRepository.Add(studentCourse);


            return RedirectToAction("Index");
        }



        // Update
        public async Task<IActionResult> UpdateStudentCourse(int Id)
        {
            var courseList = _courseRepository.GetList();
            ViewData["CourseList"] = courseList;

            var studentList = _studentRepository.GetList();
            ViewData["StudentList"] = studentList;

            var studentCourse = _studentCourseRepository.GetById(Id);
            return View(studentCourse);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStudentCourse(int id, string courseId, string studentId)
        {

            var student = _studentRepository.GetById(studentId);
            var course = _courseRepository.GetById(courseId);
            //If duplicate exists, these list are neccessary so UpdateStudentCourse page won't return null in CourseList/StudentList Viewdata
            var courseList = _courseRepository.GetList();
            ViewData["CourseList"] = courseList;

            var studentList = _studentRepository.GetList();
            ViewData["StudentList"] = studentList;

            var studentCourse = _studentCourseRepository.GetById(id);
            if (studentCourse != null)
            {
                var existing = _studentCourseRepository.GetList().Where(p => p.CourseId.Equals(courseId)
                                                                             && p.StudentId.Equals(studentId)).FirstOrDefault();
                if (existing != null && existing.Id != studentCourse.Id)
                {
                    ViewBag.Result = "Duplicate";
                    return View(studentCourse);
                }
                studentCourse.StudentId = studentId;
                studentCourse.CourseId = courseId;
                studentCourse.Student = student;
                studentCourse.Course = course;
            }


            _studentCourseRepository.Update(studentCourse);
            return RedirectToAction("Index");
        }


        //Delete
        [HttpPost]
        public async Task<IActionResult> DeleteStudentCourse(int id)
        {
            _studentCourseRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
