using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SWP_Management.Repo.Entities;

namespace SWP_Management.Repo.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly SWP_DATAContext _dbContext;

        public ProjectRepository(SWP_DATAContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Project> GetList()
        {
            return _dbContext.Projects
                .Include(p => p.Team)
                .Include(p => p.Topic)
                .ToList();
        }

        public Project GetById(int projectId)
        {
            return _dbContext.Projects
                .Include(p => p.Team)
                .Include(p => p.Topic)
                .FirstOrDefault(p => p.Id == projectId);
        }

        public void Add(Project entity)
        {
            _dbContext.Projects.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Project entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(int projectId)
        {
            var entity = _dbContext.Projects.Find(projectId);
            if (entity != null)
            {
                _dbContext.Projects.Remove(entity);
                _dbContext.SaveChanges();
            }
        }
    }

    public interface IProjectRepository
    {
        IEnumerable<Project> GetList();
        Project GetById(int projectId);
        void Add(Project entity);
        void Update(Project entity);
        void Delete(int projectId);
    }
}
