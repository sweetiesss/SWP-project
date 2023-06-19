using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SWP_Management.Repo.Entities;

namespace SWP_Management.Repo.Repositories
{
    public class SemesterRepository : ISemesterRepository
    {
        private readonly SWP_DATAContext _context;

        public SemesterRepository(SWP_DATAContext context)
        {
            _context = context;
        }

        public IEnumerable<Semester> GetList()
        {
            return _context.Semesters.Include(s => s.Courses).ToList();
        }

        public Semester GetById(string id)
        {
            return _context.Semesters.FirstOrDefault(s => s.Id == id);
        }

        public void Add(Semester semester)
        {
            _context.Semesters.Add(semester);
        }

        public void Update(Semester semester)
        {
            _context.Semesters.Update(semester);
        }

        public void Delete(Semester semester)
        {
            _context.Semesters.Remove(semester);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }

    public interface ISemesterRepository
    {
        IEnumerable<Semester> GetList();
        Semester GetById(string id);
        void Add(Semester semester);
        void Update(Semester semester);
        void Delete(Semester semester);
        void SaveChanges();
    }
}
