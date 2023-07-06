﻿using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;

namespace SWP_Management.API.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ICourseRepository _courseRepository;

        public TeamController(ITeamRepository teamRepository,
                                  ICourseRepository courseRepository)
        {
            _teamRepository = teamRepository;
            _courseRepository = courseRepository;
        }


        public IActionResult Index()
        {
            var teamList = _teamRepository.GetList();
            return View(teamList);
        }



        // Add
        public async Task<IActionResult> AddTeam()
        {
            var courseList = _courseRepository.GetList();
            ViewData["CourseList"] = courseList;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTeam(string id, string CourseId)
        {
            AddTeam();
            var course =_courseRepository.GetById(CourseId);
            var existingTeam = _teamRepository.GetById(id);
            if (existingTeam != null)
            {
                ViewBag.Result = "Duplicate";
                return View();
            }
            Team team = new Team();
            team.Course = course;
            team.CourseId = CourseId;
            team.Id = id;
            _teamRepository.Add(team);
            return RedirectToAction("Index");
        }

        // Update
        public async Task<IActionResult> UpdateTeam(string id)
        {
            var courseList = _courseRepository.GetList();
            ViewData["CourseList"] = courseList;

            var team = _teamRepository.GetById(id);
            return View(team);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTeam(string id, string CourseId)
        {
            UpdateTeam(id);
            var course = _courseRepository.GetById(CourseId);
            var team = _teamRepository.GetById(id);
            //if (team != null)
            //{
            //    ViewBag.Result = "Duplicate";
            //    return View(team);
            //}
            team.Course = course;
            team.CourseId = CourseId;
           _teamRepository.Update(team);

            return RedirectToAction("Index");
        }

        // Delete
        [HttpPost]
        public async Task<IActionResult> DeleteTeam(string id)
        {
            _teamRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
