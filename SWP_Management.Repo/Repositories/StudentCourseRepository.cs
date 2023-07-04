using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SWP_Management.Repo.Entities;

namespace SWP_Management.Repo.Repositories
{
    public class StudentCourseRepository : IStudentCourseRepository
    {
        private readonly SWP_DATAContext _dbContext;

        public StudentCourseRepository(SWP_DATAContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<StudentCourse> GetList()
        {
            return _dbContext.StudentCourses
                .Include(sc => sc.Course)
                .Include(sc => sc.Student)
                .ToList();
        }

        public StudentCourse GetById(int studentCourseId)
        {
            return _dbContext.StudentCourses
                .Include(sc => sc.Course)
                .Include(sc => sc.Student)
                .FirstOrDefault(sc => sc.Id == studentCourseId);
        }

        public void Add(StudentCourse entity)
        {
            _dbContext.StudentCourses.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(StudentCourse entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(int studentCourseId)
        {
            var entity = _dbContext.StudentCourses.Find(studentCourseId);
            if (entity != null)
            {
                _dbContext.StudentCourses.Remove(entity);
                _dbContext.SaveChanges();
            }
        }
    }

    public interface IStudentCourseRepository
    {
        IEnumerable<StudentCourse> GetList();
        StudentCourse GetById(int studentCourseId);
        void Add(StudentCourse entity);
        void Update(StudentCourse entity);
        void Delete(int studentCourseId);
    }
}
