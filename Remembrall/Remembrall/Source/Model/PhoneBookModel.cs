using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DataStorage.Source.Entity;
using DataStorage.Source.Repository;
using Remembrall.Source.ViewModel;

namespace Remembrall.Source.Model
{
    public class PhoneBookModel
    {
        private IMainRepository _repository;
        private ObservableCollection<PhoneBookRowViewModel> _phoneBookRows;

        public PhoneBookModel(IMainRepository repository)
        {
            _repository = repository;
            _phoneBookRows = UpdateRowPhoneBook();
        }

        /// <summary>
        /// Коллекция строк для телефонной книги
        /// </summary>
        public ObservableCollection<PhoneBookRowViewModel> PhoneBookRows => _phoneBookRows;

        /// <summary>
        /// Добавить человека в БД
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="surname">Фамилия</param>
        /// <param name="relation">отношение</param>
        /// <param name="phones">список телефонов</param>
        /// <param name="emails">список почт</param>
        public void AddPerson(string name, string surname, RelationshipEnum relation, IEnumerable<string> phones, IEnumerable<string> emails)
        {
            var newPerson = CreateNewPerson(name, surname, relation, phones, emails);
            _repository.PersonRepository.Add(newPerson);
            _repository.SaveChanges();
            _phoneBookRows=UpdateRowPhoneBook();
        }

        /// <summary>
        /// Удаление челвека из БД
        /// </summary>
        /// <param name="person"> человек для удаления</param>
        public void DeletePerson(Person person)
        {
            _repository.PersonRepository.Delete(person);
            _repository.SaveChanges();
            _phoneBookRows=UpdateRowPhoneBook();
        }

        #region private methods

        /// <summary>
        /// Создание нового человека из имеющихся данных
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="surname">Фамилия</param>
        /// <param name="relation">Отношение</param>
        /// <param name="phones">Набор телефонов</param>
        /// <param name="emails">Набор почт</param>
        /// <returns></returns>
        private Person CreateNewPerson(string name, string surname, RelationshipEnum relation, IEnumerable<string> phones, IEnumerable<string> emails)
        {
            var newPerson = new Person()
            {
                Name = name,
                Surname = surname,
                Relation = relation,
            };
            foreach (var phone in phones)
            {
                newPerson.Phones.Add(new Phone()
                {
                    PhoneNumber = phone
                });
            }
            foreach (var email in emails)
            {
                newPerson.Emails.Add(new Email()
                {
                    Mail = email,
                });
            }
            return newPerson;
        }

        /// <summary>
        /// Обновление коллекции строк для телефонной книги из БД
        /// </summary>
        /// <returns></returns>
        private ObservableCollection<PhoneBookRowViewModel> UpdateRowPhoneBook()
        {
            var personRows = _repository.PersonRepository.GetAllPersonsHaving()
                .Select(item => new PhoneBookRowViewModel(item))
                .ToList();
            return new ObservableCollection<PhoneBookRowViewModel>(personRows);
        }

        #endregion

    }
}
