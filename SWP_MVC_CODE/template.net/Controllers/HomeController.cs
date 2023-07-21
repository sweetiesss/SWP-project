using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Repositories;
using System.Diagnostics;
using testtemplate.Models;
using SWP_Management.Repo.Entities;
using System.ComponentModel.DataAnnotations;
using Microsoft.Identity.Client;
using System.Net;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;
//using AspNetCore;

namespace testtemplate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISemesterRepository _semesterRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IAssignmentStudenteRepository _assignmentStudentRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentTeamRepository _studentTeamRepository;
        private readonly IReportRepository _reportRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ITopicRepository _topicRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IStudentCourseRepository _studentCourseRepository;

        public HomeController(ISemesterRepository semesterRepository,
                                ICourseRepository courseRepository, 
                                IAssignmentRepository assignmentRepository,
                                IAssignmentStudenteRepository assignmentStudentRepository,
                                IStudentRepository studentRepository,
                                IStudentTeamRepository studentTeamRepository,
                                IReportRepository reportRepository,
                                IProjectRepository projectRepository,
                                ITopicRepository topicRepository,
                                ITeamRepository teamRepository,
                                IStudentCourseRepository studentCourseRepository)
        {
            _semesterRepository = semesterRepository;
            _courseRepository = courseRepository;
            _assignmentRepository = assignmentRepository;
            _assignmentStudentRepository = assignmentStudentRepository;
            _studentRepository = studentRepository;
            _studentTeamRepository = studentTeamRepository;
            _reportRepository = reportRepository;
            _projectRepository = projectRepository;
            _topicRepository = topicRepository;
            _teamRepository = teamRepository;
            _studentCourseRepository = studentCourseRepository;
        }

        public IActionResult Index1()
        {
            return View();
        }

        public IActionResult Index()
        {
            //ReadCookieCourse();
            var student = _studentRepository.GetById(ReadCookie());
            if(student == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var studentCourse = _studentCourseRepository.GetList().Where(p => p.StudentId.Equals(student.Id)).ToList();
            var semesterList = _semesterRepository.GetList().ToList();
            var sortedSemester = sortSemester(semesterList);

            List<Semester> ongoing = new List<Semester>();
            List<Semester> future = new List<Semester>();
            List<Semester> past = new List<Semester>();

            foreach(var r in sortedSemester)
            {
                if(compareSemesterDate(r) == 0)
                {
                    ongoing.Add(r);
                }
                if (compareSemesterDate(r) > 0)
                {
                    future.Add(r);
                }
                if (compareSemesterDate(r) < 0)
                {
                    past.Add(r);
                }
            }

            ViewData["SemesterList"] = sortedSemester;
            ViewData["Ongoing"] = ongoing;
            ViewData["Future"] = future;
            ViewData["Past"] = past;

            var courseList = _courseRepository.GetList().ToList();
            ViewData["CourseList"] = studentCourse;

            return View();
        }

        public int compareSemesterDate(Semester a)
        {
            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();

            string currentSemester = string.Empty;

            if (Int32.Parse(month) > 8) currentSemester += "FA";
            if (Int32.Parse(month) > 4) currentSemester += "SU";
            else currentSemester += "SP";

            currentSemester = currentSemester + year[2] + year[3];

            if (currentSemester.Equals(a.Id)) return 0;

            Semester tempSemester = new Semester
            {
                Id = currentSemester
            };

            if (compareSemester(a, tempSemester)) return 1;
            return -1;
        }

        public List<Semester> sortSemester(List<Semester> SemesterList)
        {
            for (int i = 0; i < SemesterList.Count - 1; i++)
            {
                for (int j = i + 1; j < SemesterList.Count; j++)
                {
                    if (compareSemester(SemesterList[i], SemesterList[j]))
                    {
                        var tempSemester = SemesterList[j];
                        SemesterList[j] = SemesterList[i];
                        SemesterList[i] = tempSemester;
                    }
                }
            }

            return SemesterList;
        }

        public bool compareSemester(Semester a, Semester b)
        {

            string s1 = a.Id.Substring(2);
            string s2 = b.Id.Substring(2);

            if (Int32.Parse(s1) > Int32.Parse(s2)) return true;
            if (Int32.Parse(s1) < Int32.Parse(s2)) return false;

            for (int i = 0; i < 2; i++)
            {
                if (a.Id[i].Equals('F')) return true;
                if (a.Id[i].Equals(b.Id[i]))
                {
                    if (a.Id[i + 1].Equals('U')) return true;
                }
            }

            return false;
        }




        public IActionResult HomePage(string CourseId)
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
            string studentId = ReadCookie();

			
			var student = _studentRepository.GetById(studentId);
            
            //var team = _teamRepository.GetList().Where(p => p.CourseId.Equals(CourseId)).FirstOrDefault();
            var team = _studentTeamRepository.GetList().Where(p => p.StudentId.Equals(studentId)
                                                                    && p.Team.CourseId.Equals(CourseId)).FirstOrDefault();

            if (team != null)
            {
                GetAllTaskTeam(studentId);
                var project = _projectRepository.GetList().Where(p => p.TeamId.Equals(team.TeamId)).FirstOrDefault();
                if (project != null)
                {
                    var topic = _topicRepository.GetById(project.TopicId);
                    ViewData["CurrentProject"] = topic;
                }
                var listReport = _reportRepository.GetList().Where(p => p.TeamId.Equals(team.TeamId)).ToList();
                var studentList = _studentTeamRepository.GetList().Where(p => p.TeamId.Equals(team.TeamId)).ToList();
                ViewData["ReportList"] = listReport;
                ViewData["Team"] = team;
                ViewData["StudentList"] = studentList;

            }
            else
            {

                ViewData["Team"] = null;
                ViewData["ReportList"] = null;
                ViewData["CurrentProject"] = null;
                ViewData["StudentList"] = null;
            }
            var course = _courseRepository.GetById(CourseId);
            ViewData["CourseId"] = course;
            ViewData["Student"] = student;

            //ViewData["Team"] = currentTeam;
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
            string studentId = ReadCookie();
            var student = _studentRepository.GetById(studentId);
            GetAllTaskTeam(studentId);
            var team = _teamRepository.GetList().Where(p => p.CourseId.Equals(CourseId)).FirstOrDefault();

            if (team != null) 
            {
                var project = _projectRepository.GetList().Where(p => p.TeamId.Equals(team.Id)).FirstOrDefault();
                if (project != null)
                {
                    var topic = _topicRepository.GetById(project.TopicId);
                    ViewData["CurrentProject"] = topic;
                }
                var listReport = _reportRepository.GetList().Where(p => p.TeamId.Equals(team.Id)).ToList();
                ViewData["ReportList"] = listReport;
                ViewData["Team"] = team;
                
            }
            else
            {
                ViewData["Team"] = null;
                ViewData["ReportList"] = null;
                ViewData["CurrentProject"] = null;
                ViewData["TaskList"] = null;
            }
                var course = _courseRepository.GetById(CourseId);
                ViewData["CourseId"] = course;
            
            
            //ViewData["Team"] = currentTeam;
            return View();
        }

        public IActionResult GetTask()
        {
            string studentId = ReadCookie();
            var student = _studentRepository.GetById(studentId);
            if(student.Leader == true)
            {
                GetAllTaskTeam(studentId);
                ViewBag.Result = "Leader";
                return View();
            }
            GetTaskList(studentId);

            return View();
        }  

        public void GetAllTaskTeam(string studentId)
        {
            var team = _studentTeamRepository.GetList().Where(p => p.StudentId.Equals(studentId)).FirstOrDefault();
            var taskList = _assignmentStudentRepository.GetList().Where(p => p.TeamId.Equals(team.TeamId)).ToList();




            //var team = _studentTeamRepository.GetList().Where(p => p.StudentId.Equals(studentId)).FirstOrDefault();
            //var studentList = _studentTeamRepository.GetList().Where(p => p.TeamId.Equals(team.TeamId)).ToList();
            //var assignmentList = _assignmentStudentRepository.GetList().ToList();
            //List<AssignmentStudente> list = new List<AssignmentStudente>();
            //for (int i = 0; i < studentList.Count; i++)
            //{
            //    for (int j = 0; j < assignmentList.Count; j++)
            //    {
            //        if (studentList[i].StudentId.Equals(assignmentList[j].StudentId))
            //        {
            //            list.Add(assignmentList[j]);
            //        }
            //    }
            //}


            //var taskList = _assignmentRepository.GetList().ToList();
            //List<Assignment> tasks = new List<Assignment>();
            //for (int i = 0; i < list.Count; i++)
            //{
            //    for (int j = 0; j < taskList.Count; j++)
            //    {
            //        if (list[i].TaskId.Equals(taskList[j].Id))
            //        {
            //            tasks.Add(taskList[j]);
            //        }
            //    }
            //}
            //ViewData["AssignmentStudent"] = list;
            //ViewData["TaskList"] = tasks;
            ViewData["StudentId"] = ReadCookie();
            ViewData["TaskList"] = taskList;

		}

        public void GetTaskList(string studentId)
        {   
            var assignmentList = _assignmentStudentRepository.GetList().Where(p => p.StudentId.Equals(studentId)).ToList();
           
            var taskList = _assignmentRepository.GetList().ToList();
            List<Assignment> tasks = new List<Assignment>();
            for (int i = 0; i < assignmentList.Count; i++)
            {
                for (int j = 0; j < taskList.Count; j++)
                {
                    if (assignmentList[i].TaskId.Equals(taskList[j].Id))
                    {
                        tasks.Add(taskList[j]);
                    }
                }
            }
            ViewData["AssignmentStudent"] = assignmentList;
            ViewData["TaskList"] = tasks;
        }

        public async Task<IActionResult> AddTask()
        {
            string studentId = ReadCookie();

            var student = _studentRepository.GetById(studentId);
            if(student.Leader == true)
            {
                var team = _studentTeamRepository.GetList().Where(p => p.StudentId.Equals(studentId)).FirstOrDefault();
                var studentList = _studentTeamRepository.GetList().Where(p => p.TeamId.Equals(team.TeamId)).ToList();
                ViewBag.Result = "Leader";
                ViewData["StudentList"] = studentList;
                return View();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTask(string TaskId, 
                                                string Name, 
                                                string Descripton, 
                                                DateTime DateStart, DateTime DateEnd, 
                                                string StudentId)
        {
            string studentId = ReadCookie();

            var student = _studentRepository.GetById(studentId);
            var team = _studentTeamRepository.GetList().Where(p => p.StudentId.Equals(studentId)
                                                                       && p.Team.CourseId.Equals(ReadCookieCourse())).FirstOrDefault();
            var currentTeam = _teamRepository.GetById(team.TeamId);


            Assignment assignment = new Assignment();
            AssignmentStudente assignmentStudent = new AssignmentStudente();

            var existing = _assignmentRepository.GetById(TaskId);
            if (existing != null)
            {
                ViewBag.Result = "Duplicate";
                return View();
            }

            assignment.Id = TaskId;
            assignment.Name = Name;
            assignment.Description = Descripton;
            assignment.DateStart = DateStart;
            assignment.DateEnd = DateEnd;



            if (student.Leader == true)
            {


                var selectedStudent = _studentRepository.GetById(StudentId);
                assignmentStudent.Status = "Ongoing";
                assignmentStudent.StudentId = StudentId;
                assignmentStudent.TaskId = TaskId;
                assignmentStudent.Student = selectedStudent;
                assignmentStudent.Task = assignment;
                assignmentStudent.TeamId = team.TeamId;
                assignmentStudent.Team = currentTeam;

            }
            else
            {

                assignmentStudent.Status = "Ongoing";
                assignmentStudent.StudentId = student.Id;
                assignmentStudent.TaskId = assignment.Id;
                assignmentStudent.Student = student;
                assignmentStudent.Task = assignment;
                assignmentStudent.TeamId = team.TeamId;
                assignmentStudent.Team = currentTeam;

            }

            
            _assignmentRepository.Add(assignment);

            _assignmentStudentRepository.Add(assignmentStudent);
            
            return RedirectToAction("HomePage");
        }
                
        public async Task<IActionResult> UpdateTask(string id, string studentId)
        {
            var assignmentList = _assignmentStudentRepository.GetList().Where(p => p.StudentId.Equals(studentId)).ToList();
            ViewData["AssignmentStudent"] = assignmentList;
            ViewData["StudentId"] = studentId;
            var currentAssignment = _assignmentRepository.GetById(id);
            return View(currentAssignment);
        }

        

        [HttpPost]
        public async Task<IActionResult> UpdateTask(string TaskId, string Name, string Descripton, DateTime DateStart, DateTime DateEnd, string Status, string studentId, int AssignmentStudentId)
        {
            
            var student = _studentRepository.GetById(studentId);
            var assignment = _assignmentRepository.GetById(TaskId);
            assignment.Name = Name;
            assignment.Description = Descripton;
            assignment.DateStart = DateStart;
            assignment.DateEnd = DateEnd;


            var assignmentStudent = _assignmentStudentRepository.GetById(AssignmentStudentId);

            assignmentStudent.StudentId = studentId;
            assignmentStudent.Status = Status;
            assignmentStudent.TaskId = assignment.Id;
            assignmentStudent.Student = student;
            assignmentStudent.Task = assignment;



            _assignmentStudentRepository.Update(assignmentStudent);


            _assignmentRepository.Update(assignment);
            return RedirectToAction("GetTask");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTask(string id)
        {
            _assignmentRepository.Delete(id);
            return RedirectToAction("GetTask");
        }

        public async Task<IActionResult> ViewTeam()
        {
            
            string courseId = ReadCookieCourse();
            string studentId = ReadCookie();
            //var team = _teamRepository.GetList().Where(p => p.CourseId.Equals(courseId)).FirstOrDefault();
            var team = _studentTeamRepository.GetList().Where(p => p.StudentId.Equals(studentId)
                                                                       && p.Team.CourseId.Equals(courseId)).FirstOrDefault();


            if (team != null)
            {
                var studentList = _studentTeamRepository.GetList().Where(p => p.TeamId.Equals(team.TeamId)).ToList();
                var project = _projectRepository.GetList().Where(p => p.TeamId.Equals(team.TeamId)).FirstOrDefault();
                ViewData["CurrentProject"] = project;
                ViewData["StudentList"] = studentList;
                ViewData["Team"] = team;
            }
            else
            {
                ViewData["StudentList"] = null;
                ViewData["Team"] = null;
            }
            return View();

        }

        public async Task<IActionResult> Logout()
        {
            RemoveCookie();
            return RedirectToAction("Index");
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
        public void RemoveCookie()
        {
            String key = "User";
            String value = string.Empty;
            CookieOptions cook = new CookieOptions();
            {
                cook.Expires = DateTime.Now.AddDays(-1);
            };
            Response.Cookies.Append(key, value, cook);

        }

    }

}