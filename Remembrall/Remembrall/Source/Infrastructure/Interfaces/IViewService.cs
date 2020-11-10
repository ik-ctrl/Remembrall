using System.Windows;
using DataStorage.Source.Repository;

namespace Remembrall.Source.Infrastructure.Interfaces
{
    interface IViewService
    {
        /// <summary>
        /// Мето для  редактирования праздников.
        /// </summary>
        /// <param name="owner">Родительское окно</param>
        /// <param name="repository">Репозиторий для работы с БД</param>
        void ShowHolidayWindow(IMainRepository repository, Window owner);

        /// <summary>
        /// Метод для редактирования и показа телефоной книги.
        /// </summary>
        /// <param name="owner">родительское окно</param>
        /// <param name="repository">Репозиторий для работы с БД</param>
        void ShowPhoneBook(IMainRepository repository, Window owner);
    }
}
