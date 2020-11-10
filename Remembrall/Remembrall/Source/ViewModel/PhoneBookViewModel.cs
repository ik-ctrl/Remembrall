using System;
using System.Collections.Generic;
using System.Text;
using DataStorage.Source.Entity;
using DataStorage.Source.Repository;
using Remembrall.Source.Model;

namespace Remembrall.Source.ViewModel
{
    public class PhoneBookViewModel
    {
        private PhoneBookModel _model;

        public PhoneBookViewModel(IMainRepository repository)
        {
            _model= new PhoneBookModel(repository);
        }

    }
}
