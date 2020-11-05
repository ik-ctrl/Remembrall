using DataStorage.Source.Entity;

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
        public string Month => ConvertMonthTostring(_month);

        /// <summary>
        /// Описания праздника
        /// </summary>
        public string Description => _description;

        /// <summary>
        /// Полная информация о текущем дне
        /// </summary>
        public string CurrentDayInfo => $"{_day} {Month} : {Description}";

        /// <summary>
        /// Метод для конвертации номера месяца в название месяца
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        private string ConvertMonthTostring(int month)
        {
            switch (month)
            {
                case 1:
                    return "января";
                case 2:
                    return "февраля";
                case 3:
                    return "марта";
                case 4:
                    return "апреля";
                case 5:
                    return "мая";
                case 6:
                    return "июня";
                case 7:
                    return "июля";
                case 8:
                    return "августа";
                case 9:
                    return "сентября";
                case 10:
                    return "октября";
                case 11:
                    return "ноября";
                case 12:
                    return "января";
                default:
                    {
                        return "Unknow";
                        break;
                    }

            }
        }
    }
}
