using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DataStorage.Source.Entity;
using DataStorage.Source.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataStorage.Source.Repository
{
    class SqlEmailRepository:AbsRepository<Email>,IEmailRepository
    {
        public SqlEmailRepository(DbContext context):base(context) { }

        public override Email GetItem(int id)
        {
            return _dbSet.FirstOrDefault(x => x.EmailId == id);
        }
    }
}
