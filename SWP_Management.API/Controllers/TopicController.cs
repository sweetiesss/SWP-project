using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Repositories;

namespace SWP_Management.API.Controllers
{
    public class TopicController : Controller
    {
        private readonly ITopicRepository _topicRepository;
        private readonly ILecturerRepository _lecturerRepository;

        public TopicController(ITopicRepository topicRepository,
                              ILecturerRepository lecturerRepository)
        {
            _topicRepository = topicRepository;
            _lecturerRepository = lecturerRepository;
        }


        public IActionResult Index()
        {
            var topicList = _topicRepository.GetList();
            return View(topicList);
        }



        // Add
        // Update
        // Delete
    }
}
