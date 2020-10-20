using System;
using System.Collections.Generic;
using System.Text;
using DataStorage.Source.Repository;

namespace Remembrall.Source.ViewModel
{
    public  class MainViewModel
    {
        private IMainRepository _repository;

        public MainViewModel()
        {
            _repository = new MainSqlRepository();
        }

        public string TEST => "TEST";
    }
}
