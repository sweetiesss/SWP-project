using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;
using System.Data.Common;

namespace testtemplate.Controllers
{

    public class LecturerController : Controller
    {

        private readonly ISemesterRepository _semesterRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ITopicRepository _topicRepository;
        private readonly ILecturerRepository _lecturerRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IReportRepository _reportRepository;
        private readonly IStudentTeamRepository _studentTeamRepository;
        private readonly IStudentCourseRepository _studentCourseRepository;
          


        public LecturerController(ISemesterRepository semesterRepository,
                                ICourseRepository courseRepository,
                                IStudentRepository studentRepository,
                                ITopicRepository topicRepository,
                                ILecturerRepository lecturerRepository,
                                ITeamRepository teamRepository,
                                IReportRepository reportRepository,
                                IStudentTeamRepository studentTeamRepository,
                                IStudentCourseRepository studentCourseRepository)
        {
            _semesterRepository = semesterRepository;
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
            _topicRepository = topicRepository;
            _lecturerRepository = lecturerRepository;
            _teamRepository = teamRepository;
            _reportRepository = reportRepository;
            _studentTeamRepository = studentTeamRepository;
            _studentCourseRepository = studentCourseRepository;
        }
        public IActionResult Index()
        {

            string LecturerId = ReadCookie();
            var lecturerList = _courseRepository.GetList().Where(p => p.LecturerId.Equals(LecturerId)).ToList();
            var semesterList = _semesterRepository.GetList().ToList();
            ViewData["SemesterList"] = semesterList;

            var courseList = _courseRepository.GetList().ToList();
            ViewData["CourseList"] = lecturerList;

            return View();
            
        }

        public IActionResult Dashboard(string CourseId)
        {
            string r;
            if (CourseId != null)
            {
                CreateCookie(CourseId);
            }
            else
            {
                r = ReadCookieCourse();
                CourseId = r;
            }

            string lecturerId = ReadCookie();

                ViewData["CourseId"] = CourseId;
				ViewData["cookie"] = lecturerId;

            return View();
        }

        public IActionResult GetTopic(string id)
        {
            id = ReadCookie();
            var lecturer = _lecturerRepository.GetById(id);
            if(lecturer.Leader == true)
            {
                ViewBag.Result = "Leader";
                GetAllTopicList();
                return View();
            }
            else
            {
                GetTopicList(id);
            }

            
            return View();
        }

        public void GetTopicList(string id)
        {
            var topicList = _topicRepository.GetList().Where(p => p.LecturerId.Equals(id)).ToList();
            ViewData["TopicList"] = topicList;
            ViewData["cookie"] = id;
        }

        public void GetAllTopicList()
        {
            var topicList = _topicRepository.GetList().ToList();
            ViewData["TopicList"] = topicList;
        }

        public IActionResult AddTopic()
        {
            string lecturerId = ReadCookie();
            ViewData["cookie"] = lecturerId;
            return View(); 
        }

        [HttpPost]
        public IActionResult AddTopic(string id, string name, string descripton, string lecturerId, bool approval)
        {
            GetTopicList(lecturerId);
            var lecturer = _lecturerRepository.GetById(id);
            var existing = _topicRepository.GetById(id);
            if (existing != null)
            {
                ViewBag.Result = "Duplicate";
                return View();
            }
            Topic topic = new Topic();
            topic.Id = id;
            topic.Name = name;
            topic.Description = descripton;
            topic.Approval = approval;
            topic.LecturerId = lecturerId;
            topic.Lecturer = lecturer;

            _topicRepository.Add(topic);
            return RedirectToAction("GetTopic");
        }

        public IActionResult UpdateTopic(string id)
        {
            string lecturerId = ReadCookie();
            var lecturer = _lecturerRepository.GetById(lecturerId);
            if(lecturer.Leader == true)
            {
                ViewBag.Result = "Leader";
            }
            var topic = _topicRepository.GetById(id);
            ViewData["Topic"] = topic;
            return View();
        }

        [HttpPost]
        public IActionResult UpdateTopic(string id, string name, string descripton, string lecturerId, bool approval) 
        {
            var topic = _topicRepository.GetById(id);
            var lecturer = _lecturerRepository.GetById(lecturerId);

            topic.Name=name;
            topic.Description=descripton;
            topic.Approval=approval;
            topic.LecturerId=lecturerId;
            topic.Lecturer= lecturer;

            _topicRepository.Update(topic);
            return RedirectToAction("GetTopic");
        }

        [HttpPost]
        public IActionResult DeleteTopic(string id)
        {
            _topicRepository.Delete(id);
            return RedirectToAction("GetTopic");
        }



        public IActionResult ViewReport(string courseId)
        {
            courseId = ReadCookieCourse();
            var teamList = _teamRepository.GetList().Where(p => p.CourseId.Equals(courseId)).ToList();
            ViewData["TeamList"] = teamList;
            var reportList = _reportRepository.GetList().ToList();
            List<Report> finalList = new List<Report>();
            //finalList = reportList;
            for (int i = 0; i < teamList.Count; i++)
            {
                for (int j = 0; j < reportList.Count; j++)
                {
                    if (teamList[i].Id.Equals(reportList[j].TeamId))
                    {
                        finalList.Add(reportList[j]);
                    }
                }
            }
            ViewData["ReportList"] = finalList;
            //ViewData["CourseId"] = courseId;

            return View();
        }


        public IActionResult ViewTeam()
        {
            string lecId = ReadCookie();

            var courseList = _courseRepository.GetList().Where(p => p.LecturerId.Equals(lecId)).ToList();
            var teamList = _teamRepository.GetList().ToList();

            ViewData["CourseList"] = courseList;
            ViewData["TeamList"] = teamList;
            return View();
        }

        public async Task<IActionResult> ViewAll(string CourseId)
        {
            var listStudent = _studentCourseRepository.GetList().Where(p => p.CourseId.Equals(CourseId)).ToList();
            ViewData["StudentList"] = listStudent;
            return View();
        }

        public IActionResult ViewInfoTeam(string teamId)
        {
            var studentList = _studentTeamRepository.GetList().Where(p => p.TeamId.Equals(teamId)).ToList();
            var students = _studentRepository.GetList().ToList();
            List<Student> list = new List<Student>();
            for (int i =0; i < studentList.Count; i++)
            {
                for (int j = 0; j < students.Count; j++)
                {
                    if (studentList[i].StudentId.Equals(students[j].Id))
                    {
                        list.Add(students[j]);
                    }
                }
            }
            ViewData["TeamInfo"]= list;
            return View();

        }

        public void CreateCookie(string CourseId)
        {
            String key = "Course";
            String value = CourseId;
            CookieOptions cook = new CookieOptions();
            {
                cook.Expires = DateTime.Now.AddDays(1);
            };
            Response.Cookies.Append(key, value, cook);
        }

        public string ReadCookieCourse()
        {
            String key = "Course";
            var cookieValue = Request.Cookies[key];
            ViewData["CourseId"] = cookieValue;
            return cookieValue;

        }

        public string ReadCookie()
        {
            String key = "User";
            var cookieValue = Request.Cookies[key];
            ViewData["cookieValue"] = cookieValue;
            return cookieValue;

        }   

    }
}
