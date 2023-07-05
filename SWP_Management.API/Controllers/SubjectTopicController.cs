using Microsoft.AspNetCore.Mvc;
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
        // Update
        // Delete
    }
}
