using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SWP_Management.Repo.Entities;

namespace SWP_Management.Repo.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly SWP_DATAContext _dbContext;

        public CourseRepository(SWP_DATAContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Course> GetList()
        {
            return _dbContext.Courses
                .Include(c => c.Lecturer)
                .Include(c => c.Semester)
                .Include(c => c.Subject)
                .Include(c => c.StudentCourses)
                .Include(c => c.Teams)
                .ToList();
        }

        public Course GetById(string courseId)
        {
            return _dbContext.Courses
                .Include(c => c.Lecturer)
                .Include(c => c.Semester)
                .Include(c => c.Subject)
                .Include(c => c.StudentCourses)
                .Include(c => c.Teams)
                .FirstOrDefault(c => c.Id == courseId);
        }

        public void Add(Course entity)
        {
            _dbContext.Courses.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Course entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(string courseId)
        {
            var entity = _dbContext.Courses.Find(courseId);
            if (entity != null)
            {
                _dbContext.Courses.Remove(entity);
                _dbContext.SaveChanges();
            }
        }
    }

    public interface ICourseRepository
    {
        IEnumerable<Course> GetList();
        Course GetById(string courseId);
        void Add(Course entity);
        void Update(Course entity);
        void Delete(string courseId);
    }
}
