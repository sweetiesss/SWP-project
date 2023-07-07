using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
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
        public async Task<IActionResult> AddTopic()
        {
            var lecturerList = _lecturerRepository.GetList().ToList();
            ViewData["LecturerList"] = lecturerList;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTopic(string id, string LecturerId, string name, string descripton, bool approval)
        {
            AddTopic();

            // Validation
            bool returnSwitch = false;

            if (id.Length > 50)
            {
                ViewData["IdLength"] = "IdLength";
                returnSwitch = true;
            }

            if (name.Length > 200)
            {
                ViewData["NameLength"] = "NameLength";
                returnSwitch = true;
            }

            if (descripton.Length > 200)
            {
                ViewData["DescriptionLength"] = "DescriptionLength";
                returnSwitch = true;
            }

            if (!new Validator().validate(id, @"^TP\d*$"))
            {   
                ViewData["Invalid"] = "Invalid";
                returnSwitch = true;
            }

            if(returnSwitch) return View();

            // Checl for Duplicate
            var lecturer = _lecturerRepository.GetById(LecturerId);
            var existingTopic = _topicRepository.GetById(id);
            if (existingTopic != null)
            {
                ViewBag.Result = "Duplicate";
                return View();
            }

            Topic topic = new Topic();
            topic.Id = id;
            topic.Name = name;
            topic.Approval = approval;
            topic.Description = descripton;
            topic.Lecturer = lecturer;
            topic.LecturerId = LecturerId;

            _topicRepository.Add(topic);
            return RedirectToAction("Index");
        }

        // Update
        public async Task<IActionResult> UpdateTopic(string id)
        {
            var lecturerList = _lecturerRepository.GetList().ToList();
            ViewData["LecturerList"] = lecturerList;


            var topic = _topicRepository.GetById(id);
            return View(topic);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateTopic(string id, string LecturerId, string name, string descripton, bool approval)
        {
            UpdateTopic(id);
            var lecturer = _lecturerRepository.GetById(LecturerId);
            var topic = _topicRepository.GetById(id);

            topic.Name = name;
            topic.Approval = approval;
            topic.Description = descripton;
            topic.Lecturer = lecturer;
            topic.LecturerId = LecturerId;

            _topicRepository.Update(topic);
            return RedirectToAction("Index");
        }

        // Delete
        [HttpPost]
        public async Task<IActionResult> DeleteTopic(string id)
        {
            _topicRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
