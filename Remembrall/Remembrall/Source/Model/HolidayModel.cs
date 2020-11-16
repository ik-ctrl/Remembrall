using System.Collections.ObjectModel;
using System.Linq;
using DataStorage.Source.Entity;
using DataStorage.Source.Repository;
using Microsoft.EntityFrameworkCore;
using Remembrall.Source.ViewModel;

namespace Remembrall.Source.Model
{
    public class HolidayModel
    {
        private IMainRepository _repository;
        private ObservableCollection<SpecialDateViewModel> _specialDates;

        public HolidayModel(IMainRepository repository)
        {
            _repository = repository;
            _specialDates = UpdateSpecialDateCollection(_repository);
        }

        /// <summary>
        /// Коллекция с праздниками
        /// </summary>
        public ObservableCollection<SpecialDateViewModel> SpecialDates
        {
            get => _specialDates;
        }

        /// <summary>
        /// Добавления нового праздника
        /// </summary>
        /// <param name="day"> день </param>
        /// <param name="month"> месяц </param>
        /// <param name="description"> описание праздника</param>
        public void AddSpecialDate(int day, int month, string description)
        {
            _repository.SpecialDateRepository.Add(new SpecialDate()
            {
                Day = day,
                Month = month,
                Description = description
            });
            _repository.SaveChanges();
            UpdateCollections();
        }

        /// <summary>
        /// Удаление праздника
        /// </summary>
        /// <param name="date"></param>
        public void RemoveSpecialDate(SpecialDate date)
        {
            _repository.SpecialDateRepository.Delete(date);
            _repository.SaveChanges();
            UpdateCollections();
        }

        public void CloseWindow()
        {
            _repository.Dispose();
        }

        /// <summary>
        /// Обновления коллекций
        /// </summary>
        private void UpdateCollections()
        {
            _specialDates = UpdateSpecialDateCollection(_repository);
        }

        /// <summary>
        /// Обновления коллекции праздников
        /// </summary>
        /// <param name="repos">главный репозиторий для бд</param>
        /// <returns></returns>
        private ObservableCollection<SpecialDateViewModel> UpdateSpecialDateCollection(IMainRepository repos)
        {
            var dates = repos.SpecialDateRepository
                .GetAllItem()
                .Select(item => new SpecialDateViewModel(item))
                .ToList();
            return new ObservableCollection<SpecialDateViewModel>(dates);
        }




    }
}
