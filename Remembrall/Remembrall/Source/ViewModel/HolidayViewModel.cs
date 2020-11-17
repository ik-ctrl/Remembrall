using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStorage.Source.Repository;
using Microsoft.EntityFrameworkCore;
using Remembrall.Annotations;
using Remembrall.Source.Infrastructure;
using Remembrall.Source.Model;

namespace Remembrall.Source.ViewModel
{
    public class HolidayViewModel : INotifyPropertyChanged
    {
        private HolidayModel _model;

        private ObservableCollection<string> _monthsNames;
        private int _selectedMoth;
        private string _selectedMothName;

        private ObservableCollection<int> _days;
        private int _selectedDay;

        private string _holidayDescription;

        public HolidayViewModel(IMainRepository repository)
        {
            _model = new HolidayModel(repository);
            _monthsNames = InitializeMonthsCollection();
            _days = InitializeDaysCollection();
            IntializeSelectedData();
        }

        /// <summary>
        /// Коллекция  названий  месяцев
        /// </summary>
        public ObservableCollection<string> MonthsNamesCollection => _monthsNames;

        /// <summary>
        /// Названия выбранного метода
        /// </summary>
        public string SelectedMonth
        {
            get => _selectedMothName;
            set
            {
                if (_selectedMothName.Equals(value))
                    return;
                _selectedMothName = value;
                _selectedMoth = MyConverters.GetMonthNumber(_selectedMothName);
                _days = UpdateDaysCollection(_selectedMoth);
                UpdateDaysCollectionProperty();
                _selectedDay = 1;
                UpdateInputDataProperty();

            }
        }

        /// <summary>
        /// Коллекция дней в месяце
        /// </summary>
        public ObservableCollection<int> DaysCollection => _days;

        /// <summary>
        /// Выбранный день
        /// </summary>
        public int SelectedDay
        {
            get => _selectedDay;
            set
            {
                if (_selectedDay.Equals(value))
                    return;
                _selectedDay = value;
                UpdateInputDataProperty();
            }
        }

        /// <summary>
        /// Описание нового праздника
        /// </summary>
        public string HolidayDescription
        {
            get => _holidayDescription;
            set
            {
                if (_holidayDescription.Equals(value))
                    return;

                _holidayDescription = value;
            }
        }

        /// <summary>
        /// Список всех праздников
        /// </summary>
        public ObservableCollection<SpecialDateViewModel> SpecialDates => _model.SpecialDates;

        public void ClosedWindow()
        {
            _model.CloseWindow();
        }

        #region command

        /// <summary>
        /// Поле для хранения  комманды
        /// </summary>
        private RelayCommand _addSpecialDateCommand;
        
        /// <summary>
        ///  Свойство для привязки команды 
        /// </summary>
        public RelayCommand AddSpecialDateCommand => _addSpecialDateCommand ??= new RelayCommand(obj =>
        {
            AddSpecialDate();
        });

        /// <summary>
        /// Добавление нового 
        /// </summary>
        private void AddSpecialDate()
        {
            if(string.IsNullOrEmpty(_holidayDescription))
                return;
            _model.AddSpecialDate(_selectedDay,_selectedMoth,_holidayDescription);
            UpdateSpecialDatesCollection();
        }

        /// <summary>
        /// Поле для хранения комманды удаления
        /// </summary>
        private RelayCommand _deleteSpecialDateCommand;

        /// <summary>
        /// Свойство для привязки команды удаления
        /// </summary>
        public RelayCommand DeleteSpecialDateCommand => _deleteSpecialDateCommand ??= new RelayCommand(DeleteSpecialDate);

        /// <summary>
        /// Удаление определенного праздника из списка
        /// </summary>
        /// <param name="obj">объект удаления</param>
        private void DeleteSpecialDate(object obj)
        {
            var item = obj as SpecialDateViewModel;
            _model.RemoveSpecialDate(item.SpecialDate);
            UpdateSpecialDatesCollection();
        }
        #endregion

        #region private

        /// <summary>
        /// Инициализация списка  месяцев
        /// </summary>
        /// <returns></returns>
        private ObservableCollection<string> InitializeMonthsCollection()
        {
            var list = new List<string>()
            {
                "январь",
                "февраль",
                "март",
                "апрель",
                "май",
                "июнь",
                "июль",
                "август",
                "сентябрь",
                "октябрь",
                "ноябрь",
                "декабрь"
            };
            return new ObservableCollection<string>(list);
        }

        /// <summary>
        /// Метот для инициализации коллекции дней
        /// </summary>
        /// <returns></returns>
        private ObservableCollection<int> InitializeDaysCollection()
        {
            return UpdateDaysCollection(DateTime.Now.Month);
        }

        /// <summary>
        /// Метод для обновления коллекции дней в текущем месяце
        /// </summary>
        /// <param name="currentMountNumber"></param>
        /// <returns></returns>
        private ObservableCollection<int> UpdateDaysCollection(int currentMountNumber)
        {
            var daysCount = DateTime.DaysInMonth(DateTime.Now.Year, currentMountNumber);
            var days = new List<int>();
            for (int i = 1; i < daysCount + 1; i++)
            {
                days.Add(i);
            }
            return new ObservableCollection<int>(days);
        }

        /// <summary>
        /// Инициализация начальных данных
        /// </summary>
        private void IntializeSelectedData()
        {
            _selectedMothName = string.Empty;
            _selectedDay = 1;
            _holidayDescription = string.Empty;
            _selectedMothName = MonthsNamesCollection.FirstOrDefault();
            _selectedMoth = MyConverters.GetMonthNumber(_selectedMothName);
        }

        /// <summary>
        /// Обновление выбранных  для заполнения дня для отображения
        /// </summary>
        private void UpdateInputDataProperty()
        {
            OnPropertyChanged(nameof(SelectedMonth));
            OnPropertyChanged(nameof(SelectedDay));
            OnPropertyChanged(nameof(HolidayDescription));
        }

        /// <summary>
        /// Обновление свойства коллекции дней для отображения
        /// </summary>
        private void UpdateDaysCollectionProperty()
        {
            OnPropertyChanged(nameof(DaysCollection));
        }

        /// <summary>
        /// Обновления списка праздников для отображения
        /// </summary>
        private void UpdateSpecialDatesCollection()
        {
            OnPropertyChanged(nameof(SpecialDates));
        }

        #endregion

        #region notification
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
