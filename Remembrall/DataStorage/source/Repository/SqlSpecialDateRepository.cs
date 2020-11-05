using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataStorage.Source.Entity;
using DataStorage.Source.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataStorage.Source.Repository
{
    class SqlSpecialDateRepository:AbsRepository<SpecialDate>,ISpecialDateRepository
    {
        public SqlSpecialDateRepository(DbContext context) : base(context)
        {
        }

        public override SpecialDate GetItem(int id)
        {
            return _dbSet.FirstOrDefault(item => item.SpecialDateId == id);
        }
    }
}
