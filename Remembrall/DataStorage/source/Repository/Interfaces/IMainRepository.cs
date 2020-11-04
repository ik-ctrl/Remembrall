using System;
using DataStorage.Source.Entity;
using DataStorage.Source.Repository.Interfaces;

namespace DataStorage.Source.Repository
{
    public interface IMainRepository:ICloneable,IDisposable
    {
        /// <summary>
        /// Репозиторий для работы с таблицей людей
        /// </summary>
        IPersonRepository PersonRepository { get; set; }
        /// <summary>
        /// Репозиторий для работы с таблицей людей
        /// </summary>
        INoteRepository NotesRepository { get; set; }
        /// <summary>
        /// Репозиторий для работы с таблицей людей
        /// </summary>
        IEmailRepository EmailsRepository { get; set; }
        /// <summary>
        /// Репозиторий для работы с таблицей людей
        /// </summary>
        IPhoneRepository PhonesRepository { get; set; }

        /// <summary>
        /// Репозиторий для хранения специальных дат
        /// </summary>
        ISpecialDateRepository SpecialDateRepository { get; set; }

        /// <summary>
        /// Метод для обновления контекста.
        /// </summary>
        public void ResetContext();
        /// <summary>
        /// Сохранение изменений
        /// </summary>
        public void SaveChanges();
        /// <summary>
        /// Метод для удаления базы данных
        /// </summary>
        public void DeleteDatabase();
        /// <summary>
        /// Метод для проверки подключения к БД.
        /// </summary>
        /// <returns></returns>
        public bool IsConnect();
        /// <summary>
        /// Загрузка миграций
        /// </summary>
        public void DeployMigration();
    }
}
