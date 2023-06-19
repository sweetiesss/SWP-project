using Microsoft.EntityFrameworkCore;
using SWP_Management.Repo.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SWP_Management.Repo.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly SWP_DATAContext _dbContext;

        public SubjectRepository(SWP_DATAContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Subject> GetList()
        {
            return _dbContext.Subjects.Include(s => s.Courses).ToList();
        }

        public Subject GetById(string id)
        {
            return _dbContext.Subjects.FirstOrDefault(s => s.Id == id);
        }

        public void Add(Subject subject)
        {
            _dbContext.Subjects.Add(subject);
            _dbContext.SaveChanges();
        }

        public void Update(Subject subject)
        {
            _dbContext.Subjects.Update(subject);
            _dbContext.SaveChanges();
        }

        public void Delete(string id)
        {
            var subject = _dbContext.Subjects.FirstOrDefault(s => s.Id == id);
            if (subject != null)
            {
                _dbContext.Subjects.Remove(subject);
                _dbContext.SaveChanges();
            }
        }

    }
    public interface ISubjectRepository
    {
        IEnumerable<Subject> GetList();
        Subject GetById(string id);
        void Add(Subject subject);
        void Update(Subject subject);
        void Delete(string id);
    }
}
