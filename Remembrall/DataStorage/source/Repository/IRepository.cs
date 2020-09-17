using System;
using System.Collections.Generic;

namespace DataStorage.Source.Repository
{
    public interface IRepository<T>:IDisposable where T:class
    {
        /// <summary>
        /// Получение всех элементов
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAllItem();
        /// <summary>
        /// Получение элемента по идентификатору
        /// </summary>
        /// <param name="id">идентификатор</param>
        /// <returns>объект</returns>
        T GetItem(int id);
        /// <summary>
        /// Создание нового объекта
        /// </summary>
        /// <param name="item"> новый объект</param>
        void Create(T item);
        /// <summary>
        /// Создания списка новых объектов
        /// </summary>
        /// <param name="items">новые объекты</param>
        void CreateRange(IEnumerable<T> items);
        /// <summary>
        /// Редактирование  объектов 
        /// </summary>
        /// <param name="item"> редактируемый объект</param>
        void Update(T item);
        /// <summary>
        /// Редактирует список объектов
        /// </summary>
        /// <param name="items">редактируемые объекты</param>
        void UpdateRange(IEnumerable<T> items);
        /// <summary>
        /// Удаление объекта
        /// </summary>
        /// <param name="item">удаляемый объект</param>
        void Delete(T item);
        /// <summary>
        /// Удаление объекта по идентификатору
        /// </summary>
        /// <param name="id">идентификатор объекта</param>
        void Delete(int id);
        /// <summary>
        /// Сохранения изменений в БД
        /// </summary>
        void Save();
    }
}
