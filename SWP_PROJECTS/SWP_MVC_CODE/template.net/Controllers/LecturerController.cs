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
          


        public LecturerController(ISemesterRepository semesterRepository,
                                ICourseRepository courseRepository,
                                IStudentRepository studentRepository,
                                ITopicRepository topicRepository,
                                ILecturerRepository lecturerRepository,
                                ITeamRepository teamRepository,
                                IReportRepository reportRepository)
        {
            _semesterRepository = semesterRepository;
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
            _topicRepository = topicRepository;
            _lecturerRepository = lecturerRepository;
            _teamRepository = teamRepository;
            _reportRepository = reportRepository;
        }
        public IActionResult Index()
        {
            var semesterList = _semesterRepository.GetList().ToList();
            ViewData["SemesterList"] = semesterList;

            var courseList = _courseRepository.GetList().ToList();
            ViewData["CourseList"] = courseList;

            return View();
            
        }

        public IActionResult Dashboard(string CourseId)
        {
            string lecturerId = ReadCookie();
            if (CourseId != null)
            {
                ViewData["CourseId"] = CourseId;
				ViewData["cookie"] = lecturerId;
			}

            return View();
        }

        public IActionResult GetTopic(string id)
        {
            id = ReadCookie();
            GetTopicList(id);
            return View();
        }

        public void GetTopicList(string id)
        {
            var topicList = _topicRepository.GetList().Where(p => p.LecturerId.Equals(id)).ToList();
            ViewData["TopicList"] = topicList;
            ViewData["cookie"] = id;
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
            var teamList = _teamRepository.GetList().Where(p => p.CourseId.Equals(courseId)).ToList();
            ViewData["TeamList"] = teamList;
            var reportList = _reportRepository.GetList().ToList();
            List<Report> finalList = new List<Report>();
            for (int i = 0; i < teamList.Count; i++)
            {
                for (int j = 0; j < reportList.Count; j++) {
                    if (teamList[i].Id.Equals(reportList[i].TeamId))
                    {
                        finalList.Add(reportList[j]);
                    }
                }
            }
            ViewData["ReportList"] = finalList;

            return View();
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
