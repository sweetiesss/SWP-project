using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SWP_Management.Repo.Entities;

namespace SWP_Management.Repo.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SWP_DATAContext _dbContext;

        public StudentRepository(SWP_DATAContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Student> GetList()
        {
            return _dbContext.Students
                .Include(s => s.Accounts)
                .Include(s => s.AssignmentStudents)
                .Include(s => s.StudentCourses)
                .Include(s => s.StudentTeams)
                .ToList();
        }

        public Student GetById(string studentId)
        {
            return _dbContext.Students
                .Include(s => s.Accounts)
                .Include(s => s.AssignmentStudents)
                .Include(s => s.StudentCourses)
                .Include(s => s.StudentTeams)
                .FirstOrDefault(s => s.Id == studentId);
        }

        public void Add(Student entity)
        {
            _dbContext.Students.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Student entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(string studentId)
        {
            var entity = _dbContext.Students.Find(studentId);
            if (entity != null)
            {
                _dbContext.Students.Remove(entity);
                _dbContext.SaveChanges();
            }
        }
    }

    public interface IStudentRepository
    {
        IEnumerable<Student> GetList();
        Student GetById(string studentId);
        void Add(Student entity);
        void Update(Student entity);
        void Delete(string studentId);
    }
}
