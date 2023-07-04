using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SWP_Management.Repo.Entities;

namespace SWP_Management.Repo.Repositories
{
    public class StudentTeamRepository : IStudentTeamRepository
    {
        private readonly SWP_DATAContext _dbContext;

        public StudentTeamRepository(SWP_DATAContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<StudentTeam> GetList()
        {
            return _dbContext.StudentTeams
                .Include(st => st.Student)
                .Include(st => st.Team)
                .ToList();
        }

        public StudentTeam GetById(int studentTeamId)
        {
            return _dbContext.StudentTeams
                .Include(st => st.Student)
                .Include(st => st.Team)
                .FirstOrDefault(st => st.Id == studentTeamId);
        }

        public void Add(StudentTeam entity)
        {
            _dbContext.StudentTeams.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(StudentTeam entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(int studentTeamId)
        {
            var entity = _dbContext.StudentTeams.Find(studentTeamId);
            if (entity != null)
            {
                _dbContext.StudentTeams.Remove(entity);
                _dbContext.SaveChanges();
            }
        }
    }

    public interface IStudentTeamRepository
    {
        IEnumerable<StudentTeam> GetList();
        StudentTeam GetById(int studentTeamId);
        void Add(StudentTeam entity);
        void Update(StudentTeam entity);
        void Delete(int studentTeamId);
    }
}
