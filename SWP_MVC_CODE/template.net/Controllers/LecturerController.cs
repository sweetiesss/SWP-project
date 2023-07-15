using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
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
        private readonly IProjectRepository _projectRepository;
          


        public LecturerController(ISemesterRepository semesterRepository,
                                ICourseRepository courseRepository,
                                IStudentRepository studentRepository,
                                ITopicRepository topicRepository,
                                ILecturerRepository lecturerRepository,
                                ITeamRepository teamRepository,
                                IReportRepository reportRepository,
                                IStudentTeamRepository studentTeamRepository,
                                IStudentCourseRepository studentCourseRepository,
                                IProjectRepository projectRepository)
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
            _projectRepository = projectRepository;
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
                var currentCourse = _courseRepository.GetById(CourseId);


                ViewData["CourseId"] = CourseId;
                ViewData["Course"] = currentCourse;
				ViewData["cookie"] = lecturerId;

            return View();
        }

        public IActionResult GetTopic()
        {
            string id;
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

        public IActionResult AssignTopic()
        {
            GetTopic();
            ViewTeam();

            return View();
        }

        [HttpPost]
        public IActionResult AssignTopic(string TopicId, string TeamId)
        {
            AssignTopic();
            var existing = _projectRepository.GetList().Where(p => p.TeamId.Equals(TeamId)).FirstOrDefault();
            if (existing != null)
            {
                ViewBag.Result = "Duplicate";
                return View();
            }
            var topic = _topicRepository.GetById(TopicId);
            var team = _teamRepository.GetById(TeamId);
            Project project = new Project();
            project.Topic = topic;
            project.Team = team;
            project.TopicId = TopicId;
            project.TeamId = TeamId;
            _projectRepository.Add(project);

            return RedirectToAction("GetTopic");
        }



        public IActionResult CurrentCourseTeam()
        {
            string lecId = ReadCookie();
            string courseId = ReadCookieCourse();
            
            var teamList = _teamRepository.GetList().Where(p => p.CourseId.Equals(courseId)).ToList();

            ViewData["TeamList"] = teamList;
            ViewData["Course"] = _courseRepository.GetById(courseId);
            ViewData["Project"] = _projectRepository.GetList().ToList();
            return View();
        }

        public ViewResult CurrentCourseTeamAdd() => View();

        [HttpPost]
        public IActionResult CurrentCourseTeamAdd(string TeamId)
        {
            GetTopic();
            string CourseId = ReadCookieCourse();
            var course = _courseRepository.GetById(CourseId);
            var existing = _teamRepository.GetById(TeamId);
            if (existing != null)
            {
                ViewBag.Result = "Duplicate";
                return View();
            }
            Team team = new Team();
            team.Id = TeamId;
            team.CourseId = CourseId;
            team.Course = course;
            _teamRepository.Add(team);


            return RedirectToAction("CurrentCourseTeam");
        }

        public IActionResult CurrentCourseTeamUpdate(string TeamId)
        {
            GetTopic();
            var team = _teamRepository.GetById(TeamId);
            ViewData["Project"] = _projectRepository.GetList().ToList();
            ViewData["Team"] = team;
            return View();
        }

        [HttpPost]
        public IActionResult CurrentCourseTeamUpdate(string TeamId, string TopicId)
        {
            CurrentCourseTeamUpdate(TeamId);
            var team = _teamRepository.GetById(TeamId);
            var topic = _topicRepository.GetById(TopicId);
            var project = _projectRepository.GetList().Where(p => p.TeamId.Equals(team.Id)).FirstOrDefault();
            if (project == null)
            {
                Project newProject = new Project();
                newProject.TeamId = team.Id;
                newProject.Team = team;
                newProject.Topic = topic;
                newProject.TopicId = topic.Id;

                _projectRepository.Add(newProject);
                return RedirectToAction("CurrentCourseTeam");
            }
            project.TopicId = topic.Id;
            project.Topic = topic;
            

            _projectRepository.Update(project);
            return RedirectToAction("CurrentCourseTeam");
        }

        public IActionResult CurrentCourseTeamDelete(string TeamId)
        {
            var project = _projectRepository.GetList().Where(p => p.TeamId.Equals(TeamId)).FirstOrDefault();
            if(project != null)
            {
                _projectRepository.Delete(project.Id);
            }
            _teamRepository.Delete(TeamId);
            return RedirectToAction("CurrentCourseTeam");
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
            ViewData["TeamId"] = teamId;
            ViewData["TeamInfo"]= list;
            return View();

        }

        public IActionResult AssignStudentTeam()
        {
            ViewAll(ReadCookieCourse());
            var TeamList = _teamRepository.GetList().Where(p => p.CourseId.Equals(ReadCookieCourse())).ToList();
            ViewData["TeamList"] = TeamList;
            return View();
        }

        [HttpPost]
        public IActionResult AssignStudentTeam(string StudentId, string TeamId)
        {
            AssignStudentTeam();
            var team = _teamRepository.GetById(TeamId);
            var student = _studentRepository.GetById(StudentId);
            var existing = _studentTeamRepository.GetList().Where(p => p.StudentId.Equals(StudentId) &&
                                                                       p.TeamId.Equals(TeamId)).FirstOrDefault();
            if (existing != null)
            {
                ViewBag.Result = "Duplicate";
                return View();
            }

            var currentCourse = _courseRepository.GetById(ReadCookieCourse());
            var existTeam = _studentTeamRepository.GetList().Where(p => p.StudentId.Equals(StudentId)).ToList();
            if (existTeam != null)
            {
                for (int i = 0; i < existTeam.Count; i++)
                {
                    if (existTeam[i].Team.Course.Id.Equals(currentCourse.Id))
                    {
                        ViewBag.Result = "Already Exists";
                        return View();
                    }
                }
            }
            



  
            StudentTeam studentTeam = new StudentTeam();
            studentTeam.StudentId = StudentId;
            studentTeam.TeamId = TeamId;
            studentTeam.Team = team;
            studentTeam.Student = student;

            _studentTeamRepository.Add(studentTeam);
            return RedirectToAction("CurrentCourseTeam");

        }

        [HttpPost]
        public IActionResult AssignStudentTeamDelete(string StudentId, string TeamId)
        {
            var student = _studentTeamRepository.GetList().Where(p => p.StudentId.Equals(StudentId) && p.TeamId.Equals(TeamId)).FirstOrDefault();

            _studentTeamRepository.Delete(student.Id);
            return RedirectToAction("CurrentCourseTeam");
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
