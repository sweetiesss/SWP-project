using Microsoft.EntityFrameworkCore;
using SWP_Management.Repo.Entities;
using System.Collections.Generic;
using System.Linq;

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
            return _dbContext.Assignments.ToList();
        }

        public Assignment GetById(string id)
        {
            return _dbContext.Assignments.FirstOrDefault(e => e.Id == id);
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

        public void Delete(string id)
        {
            var entity = _dbContext.Assignments.Find(id);
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
        Assignment GetById(string id);
        void Add(Assignment entity);
        void Update(Assignment entity);
        void Delete(string id);
    }
}