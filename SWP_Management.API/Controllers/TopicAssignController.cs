using Microsoft.AspNetCore.Mvc;
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
        // Update
        // Delete
    }
}
