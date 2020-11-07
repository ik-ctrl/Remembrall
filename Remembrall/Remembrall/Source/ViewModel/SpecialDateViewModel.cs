using DataStorage.Source.Entity;
using Remembrall.Source.Infrastructure;

namespace Remembrall.Source.ViewModel
{
    public class SpecialDateViewModel
    {
        private readonly SpecialDate _specialDate;
        private readonly int _day;
        private readonly int _month;
        private readonly string _description;

        public SpecialDateViewModel(SpecialDate specialDate)
        {
            _specialDate = specialDate;
            _day = _specialDate.Day;
            _month = _specialDate.Month;
            _description = _specialDate.Description;
        }

        /// <summary>
        /// День
        /// </summary>
        public int Day => _day;

        /// <summary>
        /// Месяц
        /// </summary>
        public string Month => MyConverters.ConvertMonthToString(_month);

        /// <summary>
        /// Описания праздника
        /// </summary>
        public string Description => _description;

        /// <summary>
        /// Полная информация о текущем дне
        /// </summary>
        public string CurrentDayInfo => $"{_day} {Month} : {Description}";

      
    }
}
