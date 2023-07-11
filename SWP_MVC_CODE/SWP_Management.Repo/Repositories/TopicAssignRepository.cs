using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SWP_Management.Repo.Entities;

namespace SWP_Management.Repo.Repositories
{
    public class TopicAssignRepository : ITopicAssignRepository
    {
        private readonly SWP_DATAContext _dbContext;

        public TopicAssignRepository(SWP_DATAContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<TopicAssign> GetList()
        {
            return _dbContext.TopicAssigns
                .Include(ta => ta.Semester)
                .Include(ta => ta.Subject)
                .Include(ta => ta.Topic)
                .ToList();
        }

        public TopicAssign GetById(int topicAssignId)
        {
            return _dbContext.TopicAssigns
                .Include(ta => ta.Semester)
                .Include(ta => ta.Subject)
                .Include(ta => ta.Topic)
                .FirstOrDefault(ta => ta.Id == topicAssignId);
        }

        public void Add(TopicAssign entity)
        {
            _dbContext.TopicAssigns.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(TopicAssign entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(int topicAssignId)
        {
            var entity = _dbContext.TopicAssigns.Find(topicAssignId);
            if (entity != null)
            {
                _dbContext.TopicAssigns.Remove(entity);
                _dbContext.SaveChanges();
            }
        }
    }

    public interface ITopicAssignRepository
    {
        IEnumerable<TopicAssign> GetList();
        TopicAssign GetById(int topicAssignId);
        void Add(TopicAssign entity);
        void Update(TopicAssign entity);
        void Delete(int topicAssignId);
    }
}
