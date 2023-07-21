using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SWP_Management.Repo.Entities;

namespace SWP_Management.Repo.Repositories
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly SWP_DATAContext _dbContext;

        public AssignmentRepository(SWP_DATAContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Assignment> GetList()
        {
            return _dbContext.Assignments
                .Include(a => a.AssignmentStudents)
                .ToList();
        }

        public Assignment GetById(string assignmentId)
        {
            return _dbContext.Assignments
                .Include(a => a.AssignmentStudents)
                .FirstOrDefault(a => a.Id == assignmentId);
        }

        public void Add(Assignment entity)
        {
            _dbContext.Assignments.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Assignment entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(string assignmentId)
        {
            var entity = _dbContext.Assignments.Find(assignmentId);
            if (entity != null)
            {
                _dbContext.Assignments.Remove(entity);
                _dbContext.SaveChanges();
            }
        }
    }

    public interface IAssignmentRepository
    {
        IEnumerable<Assignment> GetList();
        Assignment GetById(string assignmentId);
        void Add(Assignment entity);
        void Update(Assignment entity);
        void Delete(string assignmentId);
    }
}
