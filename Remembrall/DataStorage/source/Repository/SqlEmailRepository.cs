using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DataStorage.Source.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataStorage.Source.Repository
{
    class SqlEmailRepository:IRepository<Email>
    {
        private readonly RemembrallContext _context;

        public SqlEmailRepository(RemembrallContext context)
        {
            _context = context;
        }

        public IEnumerable<Email> GetAllItem()
        {
            return _context != null ? _context.Emails : null;
        }

        public IEnumerable<Email> GetItemByCondition(Expression<Func<Email, bool>> condition)
        {
            return _context.Emails.Where(condition).ToList();
        }

        public DbSet<Email> GetTable()
        {
            return _context.Emails;
        }

        public Email GetItem(int id)
        {
            return _context != null ? _context.Emails.FirstOrDefault(x => x.EmailId == id) : null;
        }

        public void Add(Email item)
        {
            _context?.Emails.Add(item);
        }

        public void AddRange(IEnumerable<Email> items)
        {
            _context?.Emails.AddRange(items);
        }


        public void Update(Email item)
        {
            _context?.Update(item);
        }

        public void UpdateRange(IEnumerable<Email> items)
        {
            _context?.Emails.UpdateRange(items);
        }

        public void Delete(Email item)
        {
            _context?.Emails.Remove(item);
        }

        public void DeleteRange(IEnumerable<Email> items)
        {
            _context?.Emails.RemoveRange(items);
        }

        public void Save()
        {
            _context?.SaveChanges();
        }
        
    }
}
