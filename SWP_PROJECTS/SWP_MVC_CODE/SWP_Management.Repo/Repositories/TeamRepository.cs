using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SWP_Management.Repo.Entities;

namespace SWP_Management.Repo.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly SWP_DATAContext _dbContext;

        public TeamRepository(SWP_DATAContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Team> GetList()
        {
            return _dbContext.Teams
                .Include(t => t.Course)
                .Include(t => t.Projects)
                .Include(t => t.Reports)
                .Include(t => t.StudentTeams)
                .ToList();
        }

        public Team GetById(string teamId)
        {
            return _dbContext.Teams
                .Include(t => t.Course)
                .Include(t => t.Projects)
                .Include(t => t.Reports)
                .Include(t => t.StudentTeams)
                .FirstOrDefault(t => t.Id == teamId);
        }

        public void Add(Team entity)
        {
            _dbContext.Teams.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Team entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(string teamId)
        {
            var entity = _dbContext.Teams.Find(teamId);
            if (entity != null)
            {
                _dbContext.Teams.Remove(entity);
                _dbContext.SaveChanges();
            }
        }
    }

    public interface ITeamRepository
    {
        IEnumerable<Team> GetList();
        Team GetById(string teamId);
        void Add(Team entity);
        void Update(Team entity);
        void Delete(string teamId);
    }
}
