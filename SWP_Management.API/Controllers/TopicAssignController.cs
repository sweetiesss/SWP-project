using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;

namespace SWP_Management.API.Controllers
{
    public class TopicAssignController : Controller
    {
        private readonly ITopicAssignRepository _topicAssignRepository;
        private readonly ISemesterRepository _semesterRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITopicRepository _topicRepository;

        public TopicAssignController(ITopicAssignRepository topicAssignRepository,
                              ISemesterRepository semesterRepository,
                              ISubjectRepository subjectRepository,
                              ITopicRepository topicRepository)
        {
            _topicAssignRepository = topicAssignRepository;
            _semesterRepository = semesterRepository;
            _topicRepository = topicRepository;
            _subjectRepository = subjectRepository;
        }


        public IActionResult Index()
        {
            var topicAssignList = _topicAssignRepository.GetList();
            return View(topicAssignList);
        }



        // Add

        public async Task<IActionResult> AddTopicAssign()
        {
            var semesterList = _semesterRepository.GetList().ToList();
            ViewData["SemesterList"] = semesterList;

            var subjectList = _subjectRepository.GetList().ToList();
            ViewData["SubjectList"] = subjectList;

            var topicList = _topicRepository.GetList().ToList();
            ViewData["TopicList"] = topicList;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddTopicAssign(string SemesterId, string TopicId, string SubjectId)
        {
            AddTopicAssign();
            var semester = _semesterRepository.GetById(SemesterId);
            var topic = _topicRepository.GetById(TopicId);
            var subject = _subjectRepository.GetById(SubjectId);

            var existing = _topicAssignRepository.GetList().Where(p => p.SemesterId.Equals(SemesterId)
                                                                    && p.SubjectId.Equals(SubjectId)
                                                                    && p.TopicId.Equals(TopicId)).FirstOrDefault();
            if (existing != null)
            {
                ViewBag.Result = "Duplicate";
                return View();
            }

            TopicAssign topicAssign = new TopicAssign();
            topicAssign.SubjectId = SubjectId;
            topicAssign.TopicId = TopicId;
            topicAssign.SemesterId = SemesterId;
            topicAssign.Semester = semester;
            topicAssign.Subject = subject;
            topicAssign.Topic = topic;

            _topicAssignRepository.Add(topicAssign);
            return RedirectToAction("Index");
        }


        // Update
        public async Task<IActionResult> UpdateTopicAssign(int id)
        {
            var semesterList = _semesterRepository.GetList().ToList();
            ViewData["SemesterList"] = semesterList;

            var subjectList = _subjectRepository.GetList().ToList();
            ViewData["SubjectList"] = subjectList;

            var topicList = _topicRepository.GetList().ToList();
            ViewData["TopicList"] = topicList;

            var topicAssign = _topicAssignRepository.GetById(id);
            return View(topicAssign);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTopicAssign(int id, string SemesterId, string TopicId, string SubjectId)
        {
            UpdateTopicAssign(id);
            var semester = _semesterRepository.GetById(SemesterId);
            var topic = _topicRepository.GetById(TopicId);
            var subject = _subjectRepository.GetById(SubjectId);

            var topicAssign = _topicAssignRepository.GetById(id);
            var existing = _topicAssignRepository.GetList().Where(p => p.SemesterId.Equals(SemesterId)
                                                                    && p.SubjectId.Equals(SubjectId)
                                                                    && p.TopicId.Equals(TopicId)).FirstOrDefault();
            if (existing != null && topicAssign.Id != existing.Id)
            {
                ViewBag.Result = "Duplicate";
                return View(topicAssign);
            }
            topicAssign.SubjectId = SubjectId;
            topicAssign.TopicId = TopicId;
            topicAssign.SemesterId = SemesterId;
            topicAssign.Semester = semester;
            topicAssign.Subject = subject;
            topicAssign.Topic = topic;

            _topicAssignRepository.Update(topicAssign);
            return RedirectToAction("Index");

        }

        // Delete
        [HttpPost]
        public async Task<IActionResult> DeleteTopicAssign(int id)
        {
            _topicAssignRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

