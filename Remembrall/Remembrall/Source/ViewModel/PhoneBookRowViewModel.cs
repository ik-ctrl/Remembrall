using System.Collections.Generic;
using System.Linq;
using DataStorage.Source.Entity;

namespace Remembrall.Source.ViewModel
{
    public class PhoneBookRowViewModel
    {
        private Person _person;

        private string _phones;
        private string _emails;

        public PhoneBookRowViewModel(Person person)
        {
            _person = person;
            _phones = ConvertPhoneListToString(_person.Phones);
            _emails = ConvertEmailsListToString(_person.Emails);
        }

        /// <summary>
        /// Имя человека
        /// </summary>
        public string Name => _person.Name;

        /// <summary>
        /// Фамилия человека
        /// </summary>
        public string Surname => _person.Surname;

        /// <summary>
        /// Список мобильных телефонов
        /// </summary>
        public string Phones => _phones;

        /// <summary>
        /// Список  электронных писем
        /// </summary>
        public string Emails => _emails;

        /// <summary>
        /// Степень взаимоотношений
        /// </summary>
        public RelationshipEnum Relation => _person.Relation;

        /// <summary>
        /// Метод для возвращения идентификатора человека
        /// </summary>
        /// <returns></returns>
        public int GetPersonId()
        {
            return _person.PersonId;
        }
        
        /// <summary>
        /// Метод для конвертации перечисляемых объектов типа стринг (телефонов).
        /// </summary>
        /// <param name="list">перечисляемые строки</param>
        /// <returns></returns>
        private string ConvertPhoneListToString(IEnumerable<Phone> list)
        {
            var result = string.Empty;
            if (!list.Any())
                return result;
            
            foreach (var item in list)
            {
                result += item.PhoneNumber + ";\n\r";
            }

            return result;
        }

        /// <summary>
        /// Метод для конвертации перечисляемых объектов типа стринг (электронных писем).
        /// </summary>
        /// <param name="list">перечисляемые строки</param>
        /// <returns></returns>
        private string ConvertEmailsListToString(IEnumerable<Email> list)
        {
            var result = string.Empty;
            if (!list.Any())
                return result;

            foreach (var item in list)
            {
                result += item.Mail + ";\n\r";
            }

            return result;
        }



    }
}
