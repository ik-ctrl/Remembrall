using System;
using System.Collections.Generic;
using System.Text;
using DataStorage.Source.Repository;
using Remembrall.Source.Model;

namespace Remembrall.Source.ViewModel
{
    public class HolidayViewModel
    {
        private HolidayModel _model;

        public HolidayViewModel(IMainRepository repository)
        {
            _model= new HolidayModel(repository);
        }


    }
}
