using System;
using System.Collections.Generic;
using System.Text;
using DataStorage.Source.Repository;

namespace Remembrall.Source.Model
{
    public class HolidayModel
    {
        private IMainRepository _repository;

        public HolidayModel(IMainRepository repository)
        {
            _repository = repository;
        }

    }
}
