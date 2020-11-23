using System.Collections.Generic;
using DataStorage.Source.Entity;

namespace DataStorage.Source.Repository.Interfaces
{
    public interface IPersonRepository:IRepository<Person>
    {
        public IEnumerable<Person> GetAllPersonsHaving();
    }
}
