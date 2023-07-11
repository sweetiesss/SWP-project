using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SWP_Management.Repo.Entities;

namespace SWP_Management.Repo.Repositories
{
    public class LecturerRepository : ILecturerRepository
    {
        private readonly SWP_DATAContext _dbContext;

        public LecturerRepository(SWP_DATAContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Lecturer> GetList()
        {
            return _dbContext.Lecturers
                .Include(l => l.Accounts)
                .Include(l => l.Courses)
                .Include(l => l.Topics)
                .ToList();
        }

        public Lecturer GetById(string lecturerId)
        {
            return _dbContext.Lecturers
                .Include(l => l.Accounts)
                .Include(l => l.Courses)
                .Include(l => l.Topics)
                .FirstOrDefault(l => l.Id == lecturerId);
        }

        public void Add(Lecturer entity)
        {
            _dbContext.Lecturers.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Lecturer entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(string lecturerId)
        {
            var entity = _dbContext.Lecturers.Find(lecturerId);
            if (entity != null)
            {
                _dbContext.Lecturers.Remove(entity);
                _dbContext.SaveChanges();
            }
        }
    }

    public interface ILecturerRepository
    {
        IEnumerable<Lecturer> GetList();
        Lecturer GetById(string lecturerId);
        void Add(Lecturer entity);
        void Update(Lecturer entity);
        void Delete(string lecturerId);
    }
}
