using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SWP_Management.Repo.Entities;

namespace SWP_Management.Repo.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly SWP_DATAContext _dbContext;

        public ReportRepository(SWP_DATAContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Report> GetList()
        {
            return _dbContext.Reports
                .Include(r => r.Team)
                .ToList();
        }

        public Report GetById(string reportId)
        {
            return _dbContext.Reports
                .Include(r => r.Team)
                .FirstOrDefault(r => r.Id == reportId);
        }

        public void Add(Report entity)
        {
            _dbContext.Reports.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Report entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(string reportId)
        {
            var entity = _dbContext.Reports.Find(reportId);
            if (entity != null)
            {
                _dbContext.Reports.Remove(entity);
                _dbContext.SaveChanges();
            }
        }
    }

    public interface IReportRepository
    {
        IEnumerable<Report> GetList();
        Report GetById(string reportId);
        void Add(Report entity);
        void Update(Report entity);
        void Delete(string reportId);
    }
}
