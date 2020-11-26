using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataStorage.Source.Entity;
using DataStorage.Source.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataStorage.Source.Repository
{
    public class SqlNoteRepository : AbsRepository<Note>,INoteRepository
    {
        public SqlNoteRepository(DbContext context) : base(context) { }

        public override Note GetItem(int id)
        {
            return _dbSet.FirstOrDefault(x => x.NoteId == id);
        }
    }
}
