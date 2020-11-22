using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStorage.Source.Entity;
using DataStorage.Source.Repository;
using Remembrall.Annotations;
using Remembrall.Source.Infrastructure;
using Remembrall.Source.Infrastructure.Interfaces;
using Remembrall.Source.Model;

namespace Remembrall.Source.ViewModel
{
    public class PhoneBookViewModel : INotifyPropertyChanged
    {
        private PhoneBookModel _model;
        private string _personName;
        private string _personSurname;
        private ObservableCollection<PhoneViewModel> _phonesCollection;
        private ObservableCollection<EmailViewModel> _emailsCollection;
        private RelationshipEnumViewModel _selectedRelation;


        public PhoneBookViewModel(IMainRepository repository)
        {
            _model = new PhoneBookModel(repository);
            IntializeValue();
        }

        /// <summary>
        /// Имя человека
        /// </summary>
        public string PersonName
        {
            get => _personName;
            set
            {
                _personName = value;
                OnPropertyChanged(nameof(PersonName));
            }
        }

        /// <summary>
        /// Фамилия человека
        /// </summary>
        public string PersonSurname
        {
            get => _personSurname;
            set
            {
                _personSurname = value;
                OnPropertyChanged(nameof(PersonSurname));
            }
        }

        /// <summary>
        /// Набор номеров человека
        /// </summary>
        public ObservableCollection<PhoneViewModel> PhonesCollection
        {
            get => _phonesCollection;
            set
            {
                if (_phonesCollection == value)
                    return;
                _phonesCollection = value;
            }
        }

        /// <summary>
        /// Набор почт человека
        /// </summary>
        public ObservableCollection<EmailViewModel> EmailsCollection
        {
            get => _emailsCollection;
            set
            {
                if (_emailsCollection == value)
                    return;
                _emailsCollection = value;
            }
        }

        /// <summary>
        /// Степень родства
        /// </summary>
        public RelationshipEnumViewModel SelectedRelation
        {
            get => _selectedRelation;
            set
            {
                if (_selectedRelation == value)
                    return;
                _selectedRelation = value;
                OnPropertyChanged(nameof(SelectedRelation));
            }
        }
        
        /// <summary>
        /// Записная книжка
        /// </summary>
        public ObservableCollection<PhoneBookRowViewModel> PhoneBookRows => _model.PhoneBookRows;

        #region command
        /// <summary>
        /// Можно ли добавить нового человека в БД
        /// </summary>
        /// <returns></returns>
        private bool CanAddPerson()
        {
            return !string.IsNullOrEmpty(_personName.Trim()) && !string.IsNullOrEmpty(_personSurname.Trim());
        }

        /// <summary>
        /// Поле для хранения команды
        /// </summary>
        private RelayCommand _addPersonCommand;
        
        /// <summary>
        /// Свойство для привязки команды на добавление человека
        /// </summary>
        public RelayCommand AddPersonCommand => _addPersonCommand ??= new RelayCommand(obj => { AddNewPerson(); });

        /// <summary>
        /// Добавление человека в БД
        /// </summary>
        private void AddNewPerson()
        {
            if (!CanAddPerson())
                return;
            var relation = ConvertRelationVMToRelationship(_selectedRelation);

            var phones = new List<string>();
            _phonesCollection.ToList().ForEach(item => phones.Add(item.Number));

            var emails = new List<string>();
            _emailsCollection.ToList().ForEach(item=>emails.Add(item.Email));

            _model.AddPerson(_personName, _personSurname, relation,phones, emails);
            ClearInputData();
            UpdateView();
        }

        /// <summary>
        /// Поле для хранения команды на удаленияы
        /// </summary>
        private RelayCommand _deletePersonCommand;

        /// <summary>
        /// Свойство для привязки команды на удаление
        /// </summary>
        public RelayCommand DeletePersonCommand => _deletePersonCommand ??= new RelayCommand(obj =>
        {
             DeletePerson(obj);
        });

        /// <summary>
        /// Удаления человека из записаной книжки
        /// </summary>
        /// <param name="obj">человек для удаления</param>
        private void DeletePerson(object obj)
        {
            if (obj is PhoneBookRowViewModel item)
            {
                _model.DeletePerson(item.GetPerson());
                UpdateView();
            }
        }
        #endregion

        /// <summary>
        /// Инициализация исходных значений
        /// </summary>
        private void IntializeValue()
        {
            _personName = string.Empty;
            _personSurname = string.Empty;
            _phonesCollection = new ObservableCollection<PhoneViewModel>();
            _emailsCollection = new ObservableCollection<EmailViewModel>();
        }

        /// <summary>
        /// Очистка входных данных
        /// </summary>
        private void ClearInputData()
        {
            _personName = string.Empty;
            _personSurname = string.Empty;
            _phonesCollection.Clear();
            _emailsCollection.Clear();
        }
        
        /// <summary>
        /// Обновления Окна
        /// </summary>
        private void UpdateView()
        {
            OnPropertyChanged(nameof(PersonName));
            OnPropertyChanged(nameof(PersonSurname));
            OnPropertyChanged(nameof(PhonesCollection));
            OnPropertyChanged(nameof(EmailsCollection));
            OnPropertyChanged(nameof(PhoneBookRows));
        }

        /// <summary>
        /// Конвертирует значения отношений
        /// </summary>
        /// <param name="relation"></param>
        /// <returns></returns>
        private RelationshipEnum ConvertRelationVMToRelationship(RelationshipEnumViewModel relation)
        {
            return (RelationshipEnum)relation;
        }

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
