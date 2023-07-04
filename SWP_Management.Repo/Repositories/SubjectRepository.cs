using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SWP_Management.Repo.Entities;

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
            return _dbContext.Subjects
                .Include(s => s.Courses)
                .Include(s => s.SubjectTopics)
                .Include(s => s.TopicAssigns)
                .ToList();
        }

        public Subject GetById(string subjectId)
        {
            return _dbContext.Subjects
                .Include(s => s.Courses)
                .Include(s => s.SubjectTopics)
                .Include(s => s.TopicAssigns)
                .FirstOrDefault(s => s.Id == subjectId);
        }

        public void Add(Subject entity)
        {
            _dbContext.Subjects.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Subject entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(string subjectId)
        {
            var entity = _dbContext.Subjects.Find(subjectId);
            if (entity != null)
            {
                _dbContext.Subjects.Remove(entity);
                _dbContext.SaveChanges();
            }
        }
    }

    public interface ISubjectRepository
    {
        IEnumerable<Subject> GetList();
        Subject GetById(string subjectId);
        void Add(Subject entity);
        void Update(Subject entity);
        void Delete(string subjectId);
    }
}
