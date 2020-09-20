using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataStorage.Source.Repository
{
    public interface IRepository<T> where T:class
    {
        /// <summary>
        /// Получение всех элементов
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAllItem();
        /// <summary>
        /// Поиск элементов по условию какому либо простому условию
        /// </summary>
        /// <param name="condition"> входящие условие</param>
        /// <returns></returns>
        IEnumerable<T> GetItemByCondition(Expression<Func<T, bool>> condition);
        /// <summary>
        /// Метод для возвращения таблицы (для сложных запросов)
        /// </summary>
        /// <returns></returns>
        DbSet<T> GetTable();
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
        void Add(T item);
        /// <summary>
        /// Создания списка новых объектов
        /// </summary>
        /// <param name="items">новые объекты</param>
        void AddRange(IEnumerable<T> items);
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
        /// Удаление объектов
        /// </summary>
        /// <param name="items">количество объектов </param>
        void DeleteRange(IEnumerable<T> items);
        /// <summary>
        /// Сохранения изменений в БД
        /// </summary>
        void Save();
    }
}
