﻿using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;

namespace SWP_Management.API.Controllers
{
    public class ProjectController : Controller
    {

        private readonly IProjectRepository _projectRepository;
        private readonly ITopicRepository _topicRepository;
        private readonly ITeamRepository _teamRepository;

        public ProjectController(IProjectRepository projectRepository,
                                        ITopicRepository topicRepository,
                                        ITeamRepository teamRepository)
        {
            _projectRepository = projectRepository;
            _topicRepository = topicRepository;
            _teamRepository = teamRepository;
        }


        public IActionResult Index(string TopicId)
        {
            ViewData["TopicList"] = _topicRepository.GetList();
            ViewData["ProjectList"] = _projectRepository.GetList();


            if(TopicId == null) TopicId = string.Empty;

            var projectList = _projectRepository.GetList();
            List<Project> projects = new List<Project>();

            foreach (Project pro in projectList)
            {
                if (pro.TopicId.Contains(TopicId)) projects.Add(pro);
            }

            return View(projects);
        }

        // Add
        public async Task<IActionResult> AddProject()
        {
            var listTopic = _topicRepository.GetList().ToList();
            ViewData["TopicList"] = listTopic;

            var listTeam = _teamRepository.GetList().ToList();
            ViewData["TeamList"] = listTeam;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProject(string topicId, string teamId)
        {
            AddProject();
            var team = _teamRepository.GetById(teamId);
            var topic =_topicRepository.GetById(topicId);
            var existingProject = _projectRepository.GetList().Where(p => p.TeamId.Equals(teamId)).FirstOrDefault();
            if (existingProject != null)
            {
                ViewBag.Result = "Duplicate";
                return View();
            }
            Project project = new Project();
            project.TopicId = topicId;
            project.TeamId = teamId;
            project.Team = team;
            project.Topic = topic;

            _projectRepository.Add(project);
            return RedirectToAction("Index");
        }
        // Update
        public async Task<IActionResult> UpdateProject(int id)
        {
            var listTopic = _topicRepository.GetList().ToList();
            ViewData["TopicList"] = listTopic;

            var listTeam = _teamRepository.GetList().ToList();
            ViewData["TeamList"] = listTeam;

            var project =_projectRepository.GetById(id);
            return View(project);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProject(int id, string teamId, string topicId)
        {
            UpdateProject(id);
            var team = _teamRepository.GetById(teamId);
            var topic = _topicRepository.GetById(topicId);

            var project = _projectRepository.GetById(id);
            if (project != null) 
            {
                var existingProject = _projectRepository.GetList().Where(p => p.TeamId.Equals(teamId)).FirstOrDefault();
                if (existingProject != null)
                {
                    if (existingProject.Id != id)
                    {
                        ViewBag.Result = "Duplicate";
                        return View(project);
                    }
                }
                project.Team = team;
                project.Topic = topic;
                project.TeamId = teamId;
                project.TopicId = topicId;
            }
            _projectRepository.Update(project);
            return RedirectToAction("Index");

        }
        // Delete
        [HttpPost]
        public async Task<IActionResult> DeleteProject(int id)
        {
            _projectRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
