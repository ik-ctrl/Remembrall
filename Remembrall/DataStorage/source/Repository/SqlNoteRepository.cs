using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataStorage.Source.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataStorage.Source.Repository
{
    public class SqlNoteRepository : IRepository<Note>
    {
        private readonly RemembrallContext _context;

        public SqlNoteRepository(RemembrallContext context)
        {
            _context = context;
        }
        
        public IEnumerable<Note> GetAllItem()
        {
            return  _context != null ? _context.Notes: null;
        }

        public IEnumerable<Note> GetItemByCondition(Expression<Func<Note, bool>> condition)
        {
            return _context.Notes.Where(condition).ToList();
        }

        public DbSet<Note> GetTable()
        {
            return _context.Notes;
        }

        public Note GetItem(int id)
        {
            return _context != null ? _context.Notes.FirstOrDefault(x => x.NoteId == id) : null;
        }

        public void Add(Note item)
        {
            _context?.Notes.Add(item);
        }

        public void AddRange(IEnumerable<Note> items)
        {
           _context?.Notes.AddRange(items);
        }

        public void Update(Note item)
        {
            _context?.Update(item);
        }

        public void UpdateRange(IEnumerable<Note> items)
        {
            _context?.Notes.UpdateRange(items);
        }

        public void Delete(Note item)
        {
            _context?.Notes.Remove(item);
        }

        public void DeleteRange(IEnumerable<Note> items)
        {
            _context?.Notes.RemoveRange(items);
        }

        public void Save()
        {
            _context?.SaveChanges();
        }

    }
}
