using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;

namespace SWP_Management.API.Controllers
{
    public class SubjectTopicController : Controller
    {
        private readonly ISubjectTopicRepository _subjectTopicRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITopicRepository _topicRepository;

        public SubjectTopicController(ISubjectTopicRepository subjectTopicRepository,
                                 ISubjectRepository subjectRepository,
                                 ITopicRepository topicRepository)
        {
            _subjectTopicRepository = subjectTopicRepository;
            _subjectRepository = subjectRepository;
            _topicRepository = topicRepository;
        }


        public IActionResult Index()
        {
            var subjectTopicList = _subjectTopicRepository.GetList();
            return View(subjectTopicList);
        }



        // Add
        public async Task<IActionResult> AddSubjectTopic()
        {
            var subjectList = _subjectRepository.GetList().ToList();
            ViewData["SubjectList"] = subjectList;

            var topicList = _topicRepository.GetList().ToList();
            ViewData["TopicList"] = topicList;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSubjectTopic(int id, string SubjectId, string TopicId)
        {
            AddSubjectTopic();
            var subject = _subjectRepository.GetById(SubjectId);
            var topic = _topicRepository.GetById(TopicId);

            var existing = _subjectTopicRepository.GetList().Where(p => p.SubjectId.Equals(SubjectId)
                                                                     && p.TopicId.Equals(TopicId)).FirstOrDefault();
            if (existing != null)
            {
                ViewBag.Result = "Duplicate";
                return View();
            }
            SubjectTopic subjectTopic = new SubjectTopic();
            subjectTopic.SubjectId = SubjectId;
            subjectTopic.TopicId = TopicId;
            subjectTopic.Subject = subject;
            subjectTopic.Topic = topic;

            _subjectTopicRepository.Add(subjectTopic);
            return RedirectToAction("Index");
        }

        // Update
        public async Task<IActionResult> UpdateSubjectTopic(int id)
        {
            var subjectList = _subjectRepository.GetList().ToList();
            ViewData["SubjectList"] = subjectList;

            var topicList = _topicRepository.GetList().ToList();
            ViewData["TopicList"] = topicList;

            var subjectTopic = _subjectTopicRepository.GetById(id);
            return View(subjectTopic);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSubjectTopic(int id, string SubjectId, string TopicId)
        {
            UpdateSubjectTopic(id);
            var subject = _subjectRepository.GetById(SubjectId);
            var topic = _topicRepository.GetById(TopicId);

            var subjectTopic = _subjectTopicRepository.GetById(id);
            var existing = _subjectTopicRepository.GetList().Where(p => p.SubjectId.Equals(SubjectId)
                                                             && p.TopicId.Equals(TopicId)).FirstOrDefault();
            if(existing != null && existing.Id != subjectTopic.Id) 
                {
                ViewBag.Result = "Duplicate";
                return View(subjectTopic);
            }
            subjectTopic.Topic = topic;
            subjectTopic.SubjectId = SubjectId;
            subjectTopic.TopicId = TopicId;
            subjectTopic.Subject = subject;

            _subjectTopicRepository.Update(subjectTopic);
            return RedirectToAction("Index");
        }

        // Delete

        [HttpPost]
        public async Task<IActionResult> DeleteSubjectTopic(int id)
        {
            _subjectTopicRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
