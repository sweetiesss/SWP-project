using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SWP_Management.Repo.Entities;

namespace SWP_Management.Repo.Repositories
{
    public class SemesterRepository : ISemesterRepository
    {
        private readonly SWP_DATAContext _dbContext;

        public SemesterRepository(SWP_DATAContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Semester> GetList()
        {
            return _dbContext.Semesters
                .Include(s => s.Courses)
                .Include(s => s.TopicAssigns)
                .ToList();
        }

        public Semester GetById(string semesterId)
        {
            return _dbContext.Semesters
                .Include(s => s.Courses)
                .Include(s => s.TopicAssigns)
                .FirstOrDefault(s => s.Id == semesterId);
        }

        public void Add(Semester entity)
        {
            _dbContext.Semesters.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Semester entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(string semesterId)
        {
            var entity = _dbContext.Semesters.Find(semesterId);
            if (entity != null)
            {
                _dbContext.Semesters.Remove(entity);
                _dbContext.SaveChanges();
            }
        }
    }

    public interface ISemesterRepository
    {
        IEnumerable<Semester> GetList();
        Semester GetById(string semesterId);
        void Add(Semester entity);
        void Update(Semester entity);
        void Delete(string semesterId);
    }
}
