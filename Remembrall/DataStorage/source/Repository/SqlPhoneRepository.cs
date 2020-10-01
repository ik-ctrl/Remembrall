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
    class SqlPhoneRepository:AbsRepository<Phone>,IPhoneRepository
    {
        public SqlPhoneRepository(DbContext context) : base(context) { }

        public override Phone GetItem(int id)
        {
            return _dbSet.FirstOrDefault(x => x.PhoneId == id);
        }
    }
}
