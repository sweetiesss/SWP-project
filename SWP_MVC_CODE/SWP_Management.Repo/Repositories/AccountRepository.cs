using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SWP_Management.Repo.Entities;

namespace SWP_Management.Repo.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly SWP_DATAContext _dbContext;

        public AccountRepository(SWP_DATAContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Account> GetList()
        {
            return _dbContext.Accounts
                .Include(a => a.Student)
                .Include(a => a.Teacher)
                .ToList();
        }

        public Account GetById(int accountId)
        {
            return _dbContext.Accounts
                .Include(a => a.Student)
                .Include(a => a.Teacher)
                .FirstOrDefault(a => a.Id == accountId);
        }

        public void Add(Account entity)
        {
            _dbContext.Accounts.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Account entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(int accountId)
        {
            var entity = _dbContext.Accounts.Find(accountId);
            if (entity != null)
            {
                _dbContext.Accounts.Remove(entity);
                _dbContext.SaveChanges();
            }
        }
    }

    public interface IAccountRepository
    {
        IEnumerable<Account> GetList();
        Account GetById(int accountId);
        void Add(Account entity);
        void Update(Account entity);
        void Delete(int accountId);
    }
}
