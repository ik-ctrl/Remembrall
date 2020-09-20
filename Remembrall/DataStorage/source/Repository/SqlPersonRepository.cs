using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DataStorage.Source.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataStorage.Source.Repository
{
    class SqlPersonRepository:IRepository<Person>
    {
        private readonly RemembrallContext _context;

        public SqlPersonRepository(RemembrallContext context)
        {
            _context = context;
        }

        public IEnumerable<Person> GetAllItem()
        {
            return _context != null ? _context.People.ToList() : null;
        }

        public IEnumerable<Person> GetItemByCondition(Expression<Func<Person, bool>> condition)
        {
            return _context.People.Where(condition).ToList();
        }

        public DbSet<Person> GetTable()
        {
            return _context.People;
        }

        public Person GetItem(int id)
        {
            return _context != null ? _context.People.FirstOrDefault(x => x.PersonId == id) : null;
        }

        public void Add(Person item)
        {
            _context?.People.Add(item);
        }

        public void AddRange(IEnumerable<Person> items)
        {
            _context?.People.AddRange(items);
        }

        public void Update(Person item)
        {
            _context?.Update(item);
        }

        public void UpdateRange(IEnumerable<Person> items)
        {
            _context?.People.UpdateRange(items);
        }

        public void Delete(Person item)
        {
            _context?.People.Remove(item);
        }

        public void DeleteRange(IEnumerable<Person> items)
        {
            _context?.People.RemoveRange(items);
        }

        public void Save()
        {
            _context?.SaveChanges();
        }

    }
}
