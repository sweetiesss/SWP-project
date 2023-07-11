using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SWP_Management.Repo.Entities;

namespace SWP_Management.Repo.Repositories
{
    public class SubjectTopicRepository : ISubjectTopicRepository
    {
        private readonly SWP_DATAContext _dbContext;

        public SubjectTopicRepository(SWP_DATAContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<SubjectTopic> GetList()
        {
            return _dbContext.SubjectTopics
                .Include(st => st.Subject)
                .Include(st => st.Topic)
                .ToList();
        }

        public SubjectTopic GetById(int subjectTopicId)
        {
            return _dbContext.SubjectTopics
                .Include(st => st.Subject)
                .Include(st => st.Topic)
                .FirstOrDefault(st => st.Id == subjectTopicId);
        }

        public void Add(SubjectTopic entity)
        {
            _dbContext.SubjectTopics.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(SubjectTopic entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(int subjectTopicId)
        {
            var entity = _dbContext.SubjectTopics.Find(subjectTopicId);
            if (entity != null)
            {
                _dbContext.SubjectTopics.Remove(entity);
                _dbContext.SaveChanges();
            }
        }
    }

    public interface ISubjectTopicRepository
    {
        IEnumerable<SubjectTopic> GetList();
        SubjectTopic GetById(int subjectTopicId);
        void Add(SubjectTopic entity);
        void Update(SubjectTopic entity);
        void Delete(int subjectTopicId);
    }
}
