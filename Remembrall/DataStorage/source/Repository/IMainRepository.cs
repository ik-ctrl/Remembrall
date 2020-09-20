using System;
using DataStorage.Source.Entity;

namespace DataStorage.Source.Repository
{
    public interface IMainRepository:ICloneable,IDisposable
    {
        /// <summary>
        /// Репозиторий для работы с таблицей людей
        /// </summary>
        IRepository<Person> PersonRepository { get; set; }
        /// <summary>
        /// Репозиторий для работы с таблицей людей
        /// </summary>
        IRepository<Note> NotesRepository { get; set; }
        /// <summary>
        /// Репозиторий для работы с таблицей людей
        /// </summary>
        IRepository<Email> EmailsRepository { get; set; }
        /// <summary>
        /// Репозиторий для работы с таблицей людей
        /// </summary>
        IRepository<Phone> PhonesRepository { get; set; }
        /// <summary>
        /// Метод для обновления контекста.
        /// </summary>
        public void ResetContext();

    }
}
