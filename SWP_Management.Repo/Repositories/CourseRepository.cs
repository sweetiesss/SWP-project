using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SWP_Management.Repo.Entities;

namespace SWP_Management.Repo.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly SWP_DATAContext _context;

        public CourseRepository(SWP_DATAContext context)
        {
            _context = context;
        }

        public IEnumerable<Course> GetList()
        {
            return _context.Courses.ToList();
        }

        public Course GetById(string id)
        {
            return _context.Courses.FirstOrDefault(c => c.Id == id);
        }

        public void Add(Course course)
        {
            _context.Courses.Add(course);
        }

        public void Update(Course course)
        {
            _context.Courses.Update(course);
        }

        public void Delete(Course course)
        {
            _context.Courses.Remove(course);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Course> GetCoursesBySemesterId(string semesterId)
        {
            return _context.Courses.Where(c => c.SemesterId == semesterId).ToList();
        }
        public void RemoveCourseFromSemester(string semesterId, string courseId)
        {
            var course = _context.Courses.FirstOrDefault(c => c.SemesterId == semesterId && c.Id == courseId);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }
        }
    }

    public interface ICourseRepository
    {
        IEnumerable<Course> GetList();
        Course GetById(string id);
        void Add(Course course);
        void Update(Course course);
        void Delete(Course course);
        void SaveChanges();
        IEnumerable<Course> GetCoursesBySemesterId(string semesterId);
        void RemoveCourseFromSemester(string semesterId, string courseId);

    }
}
