using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DataStorage.Source.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataStorage.Source.Repository
{
    class SqlPhoneRepository:IRepository<Phone>
    {
        private readonly RemembrallContext _context;

        public SqlPhoneRepository(RemembrallContext context)
        {
            _context = context;
        }

        public IEnumerable<Phone> GetAllItem()
        {
            return _context != null ? _context.Phones.ToList() : null;
        }

        public IEnumerable<Phone> GetItemByCondition(Expression<Func<Phone, bool>> condition)
        {
            return _context.Phones.Where(condition).ToList();
        }

        public DbSet<Phone> GetTable()
        {
            return _context.Phones;
        }

        public Phone GetItem(int id)
        {
            return _context != null ? _context.Phones.FirstOrDefault(x => x.PhoneId == id) : null;
        }

        public void Add(Phone item)
        {
            _context?.Phones.Add(item);
        }

        public void AddRange(IEnumerable<Phone> items)
        {
            _context?.Phones.AddRange(items);
        }

        public void Update(Phone item)
        {
            _context?.Update(item);
        }

        public void UpdateRange(IEnumerable<Phone> items)
        {
            _context?.Phones.UpdateRange(items);
        }

        public void Delete(Phone item)
        {
            _context?.Phones.Remove(item);
        }

        public void DeleteRange(IEnumerable<Phone> items)
        {
            _context?.Phones.RemoveRange(items);
        }

        public void Save()
        {
            _context?.SaveChanges();
        }

    }
}
