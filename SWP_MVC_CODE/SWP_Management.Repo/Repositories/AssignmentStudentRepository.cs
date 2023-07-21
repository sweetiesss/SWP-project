using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SWP_Management.Repo.Entities;

namespace SWP_Management.Repo.Repositories
{
    public class AssignmentStudentRepository : IAssignmentStudenteRepository
    {
        private readonly SWP_DATAContext _dbContext;

        public AssignmentStudentRepository(SWP_DATAContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<AssignmentStudente> GetList()
        {
            return _dbContext.AssignmentStudents
                .Include(a => a.Student)
                .Include(a => a.Task)
                .Include(a => a.Team)
                .ToList();
        }

        public AssignmentStudente GetById(int assignmentStudentId)
        {
            return _dbContext.AssignmentStudents
                .Include(a => a.Student)
                .Include(a => a.Task)
                .Include(a => a.Team)
                .FirstOrDefault(a => a.Id == assignmentStudentId);
        }

        public void Add(AssignmentStudente entity)
        {
            _dbContext.AssignmentStudents.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(AssignmentStudente entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(int assignmentStudentId)
        {
            var entity = _dbContext.AssignmentStudents.Find(assignmentStudentId);
            if (entity != null)
            {
                _dbContext.AssignmentStudents.Remove(entity);
                _dbContext.SaveChanges();
            }
        }
    }

    public interface IAssignmentStudenteRepository
    {
        IEnumerable<AssignmentStudente> GetList();
        AssignmentStudente GetById(int assignmentStudentId);
        void Add(AssignmentStudente entity);
        void Update(AssignmentStudente entity);
        void Delete(int assignmentStudentId);
    }
}
