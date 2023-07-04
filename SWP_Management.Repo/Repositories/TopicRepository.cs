using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SWP_Management.Repo.Entities;

namespace SWP_Management.Repo.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private readonly SWP_DATAContext _dbContext;

        public TopicRepository(SWP_DATAContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Topic> GetList()
        {
            return _dbContext.Topics
                .Include(t => t.Lecturer)
                .Include(t => t.Projects)
                .Include(t => t.SubjectTopics)
                .Include(t => t.TopicAssigns)
                .ToList();
        }

        public Topic GetById(string topicId)
        {
            return _dbContext.Topics
                .Include(t => t.Lecturer)
                .Include(t => t.Projects)
                .Include(t => t.SubjectTopics)
                .Include(t => t.TopicAssigns)
                .FirstOrDefault(t => t.Id == topicId);
        }

        public void Add(Topic entity)
        {
            _dbContext.Topics.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Topic entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(string topicId)
        {
            var entity = _dbContext.Topics.Find(topicId);
            if (entity != null)
            {
                _dbContext.Topics.Remove(entity);
                _dbContext.SaveChanges();
            }
        }
    }

    public interface ITopicRepository
    {
        IEnumerable<Topic> GetList();
        Topic GetById(string topicId);
        void Add(Topic entity);
        void Update(Topic entity);
        void Delete(string topicId);
    }
}
