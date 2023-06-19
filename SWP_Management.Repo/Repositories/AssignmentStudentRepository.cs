using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SWP_Management.Repo.Entities;

namespace SWP_Management.Repo.Repositories
{
    public class AssignmentStudentRepository : IAssignmentStudentRepository
    {
        private readonly SWP_DATAContext _dbContext;

        public AssignmentStudentRepository(SWP_DATAContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<AssignmentStudent> GetList()
        {
            return _dbContext.AssignmentStudents
                .Include(a => a.Student)
                .Include(a => a.Task)
                .ToList();
        }

        public AssignmentStudent GetById(string taskId, string studentId)
        {
            return _dbContext.AssignmentStudents
                .Include(a => a.Student)
            .Include(a => a.Task)
                .FirstOrDefault(a => a.TaskId == taskId && a.StudentId == studentId);
        }

        public void Add(AssignmentStudent entity)
        {
            _dbContext.AssignmentStudents.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(AssignmentStudent entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(string taskId, string studentId)
        {
            var entity = _dbContext.AssignmentStudents.Find(taskId, studentId);
            if (entity != null)
            {
                _dbContext.AssignmentStudents.Remove(entity);
                _dbContext.SaveChanges();
            }
        }
    }

    public interface IAssignmentStudentRepository
    {
        IEnumerable<AssignmentStudent> GetList();
        AssignmentStudent GetById(string taskId, string studentId);
        void Add(AssignmentStudent entity);
        void Update(AssignmentStudent entity);
        void Delete(string taskId, string studentId);
    }
}
