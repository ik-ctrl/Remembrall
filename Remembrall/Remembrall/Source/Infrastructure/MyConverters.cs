namespace Remembrall.Source.Infrastructure
{
    public static class MyConverters
    {
        /// <summary>
        /// Метод для конвертации номера месяца в название месяца c учетом падежа
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public static string ConvertMonthToString(int month)
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

        /// <summary>
        /// Конвертация  номера месяца в  название месяца без учета падежа
        /// </summary>
        /// <param name="month">номер месяца</param>
        /// <returns></returns>
        public static string GetMonthName(int month)
        {
            switch (month)
            {
                case 1:
                    return "январь";
                case 2:
                    return "февраль";
                case 3:
                    return "март";
                case 4:
                    return "апрель";
                case 5:
                    return "май";
                case 6:
                    return "июнь";
                case 7:
                    return "июль";
                case 8:
                    return "август";
                case 9:
                    return "сентябрь";
                case 10:
                    return "октябрь";
                case 11:
                    return "ноябрь";
                case 12:
                    return "января";
                default:
                    {
                        return "Unknow";
                        break;
                    }
            }
        }

        /// <summary>
        /// Конвертация  названия месяца в номер
        /// </summary>
        /// <param name="monthName"> нназвания месяца</param>
        /// <returns></returns>
        public static int GetMonthNumber(string monthName)
        {
            switch (monthName)
            {
                case "январь":
                    return 1;
                case "февраль":
                    return 2;
                case "март":
                    return 3;
                case "апрель":
                    return 4;
                case "май":
                    return 5;
                case "июнь":
                    return 6;
                case "июль":
                    return 7;
                case "август":
                    return 8;
                case "сентябрь":
                    return 9;
                case "октябрь":
                    return 10;
                case "ноябрь":
                    return 11;
                case "января":
                    return 12;
                default:
                    {
                        return -1;
                    }
            }
        }
    }
}
