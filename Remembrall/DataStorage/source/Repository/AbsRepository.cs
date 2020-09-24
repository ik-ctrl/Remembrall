using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DataStorage.Source.Repository
{
    public  abstract class AbsRepository<T> : IRepository<T> where T :class
    {
        private readonly DbContext _context;
        protected DbSet<T> _dbSet;
        protected AbsRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IQueryable<T> Query()
        {
            return _dbSet;
        }

        public IEnumerable<T> GetAllItem()
        {
            return _dbSet.ToArray();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> condition)
        {
            return _dbSet.Where(condition).ToList();
        }

        public abstract T GetItem(int id);
        
        public void Add(T item)
        {
             _dbSet.Add(item);
        }

        public void AddRange(IEnumerable<T> items)
        {
            _dbSet.AddRange(items);
        }

        public void Update(T item)
        {
            _dbSet.Update(item);
        }

        public void UpdateRange(IEnumerable<T> items)
        {
            _dbSet.UpdateRange(items);
        }

        public void Delete(T item)
        {
            _dbSet.Remove(item);
        }

        public void DeleteRange(IEnumerable<T> items)
        {
            _dbSet.RemoveRange(items);
        }

    }
}
