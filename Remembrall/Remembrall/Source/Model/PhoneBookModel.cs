using System;
using System.Collections.Generic;
using System.Text;
using DataStorage.Source.Repository;

namespace Remembrall.Source.Model
{
    public class PhoneBookModel
    {
        private IMainRepository _repository;

        public PhoneBookModel(IMainRepository repository)
        {
            _repository = repository;
        }
    }
}
