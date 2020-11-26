using System;
using System.Collections.Generic;
using System.Text;
using DataStorage.Source.Entity;

namespace Remembrall.Source.ViewModel
{
    public class PhoneViewModel
    {
        private string _number;
        public string Number { get => _number; set => _number = value; }
    }
}
