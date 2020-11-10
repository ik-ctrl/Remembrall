using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using DataStorage.Source.Entity;
using DataStorage.Source.Repository;
using Remembrall.Annotations;
using Remembrall.Source.Infrastructure;
using Remembrall.Source.Infrastructure.Interfaces;
using Remembrall.Source.Model;

namespace Remembrall.Source.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly AppModel _model;
        private readonly Window _mainWindow;
        private readonly Timer _autoUpdateViewTimer;
        private string _noteDescription;
        private IViewService _viewService;
        public MainViewModel(Window mainWindow)
        {

            _model = new AppModel();
            _viewService = new ViewService();
            _autoUpdateViewTimer = new Timer(new TimerCallback(AutoUpdateView), null, 0, 250);
            _noteDescription = string.Empty;
            _mainWindow = mainWindow;
        }

        #region notes

        /// <summary>
        ///  Индикатор показа выполенных задач
        /// </summary>
        public bool ShowCompletedCollection => _model.CompletedNotesCollection.Count != 0;

        /// <summary>
        /// Тект заметки
        /// </summary>
        public string NoteDescription
        {
            get => _noteDescription;
            set => _noteDescription = value;
        }

        /// <summary>
        /// Коллекция невыполненных заметок
        /// </summary>
        public ObservableCollection<NoteItemViewModel> IncompletedNotesCollection
        {
            get => _model.IncompletedNotesCollection;
        }

        /// <summary>
        /// Коллекция выполненных заметок
        /// </summary>
        public ObservableCollection<NoteItemViewModel> CompletedNotesCollection
        {
            get => _model.CompletedNotesCollection;
        }

        /// <summary>
        /// Поле для хранения команды добавления новой заметки
        /// </summary>
        private RelayCommand _addNoteCommand;

        /// <summary>
        /// Свойство для обращения к команде добавления новой заметки
        /// </summary>
        public RelayCommand AddNoteCommand => _addNoteCommand ?? new RelayCommand(obj =>
       {
           AddNote();
       });

        /// <summary>
        /// Метод для добавления новой заметки в БД
        /// </summary>
        private void AddNote()
        {
            if (string.IsNullOrEmpty(_noteDescription.Trim()))
                return;
            _model.AddNote(_noteDescription.Trim());
            _noteDescription = string.Empty;
            UpdateView();
        }

        /// <summary>
        /// Поле для хранение команды для удаления всей коллекции невыполненных заметок
        /// </summary>
        private RelayCommand _removeIncompletedNotesCommand;

        /// <summary>
        /// Свойство для обращения к команде для удаления всей коллекции невыполненных заметок
        /// </summary>
        public RelayCommand RemoveIncompletedNotesCommand =>
            _removeIncompletedNotesCommand ??= new RelayCommand(obj => { RemoveIncompletedNotes(); },
                                    obj => { return IncompletedNotesCollection.Count != 0; });

        /// <summary>
        /// Метод для удаления коллекции невыполненных заметок
        /// </summary>
        private void RemoveIncompletedNotes()
        {
            var notesVm = IncompletedNotesCollection.ToList();
            _model.RemoveNotes(ConvertNoteItemViewModelToNote(notesVm));
            UpdateView();
        }

        /// <summary>
        /// Поле для хранение команды для удаления всей коллекции выполненных заметок
        /// </summary>
        private RelayCommand _removeCompletedNotesCommand;

        /// <summary>
        /// Свойство для обращения к команде для удаления всей коллекции выполненных заметок
        /// </summary>
        public RelayCommand RemoveCompletedNotesCommand =>
            _removeCompletedNotesCommand ??= new RelayCommand(obj => { RemoveCompletedNotes(); },
                obj => { return CompletedNotesCollection.Count != 0; });

        /// <summary>
        /// Метод для удаления коллекции выполненных заметок
        /// </summary>
        private void RemoveCompletedNotes()
        {
            var notesVm = CompletedNotesCollection.ToList();
            _model.RemoveNotes(ConvertNoteItemViewModelToNote(notesVm));
            UpdateView();
        }

        /// <summary>
        /// Поле для хранение команды для удаления определенной заметки
        /// </summary>
        private RelayCommand _removeSpecialNoteCommand;

        /// <summary>
        /// Свойство для обращения к команде для удаления определенной заметки
        /// </summary>
        public RelayCommand RemoveSpecialNoteCommand =>
            _removeSpecialNoteCommand ??= new RelayCommand(obj => { RemoveSpecialNote(obj); });

        /// <summary>
        /// Метод для удаления определенной заметки
        /// </summary>
        private void RemoveSpecialNote(object obj)
        {
            if (obj is NoteItemViewModel note)
            {
                _model.RemoveNote(note.Note);
                UpdateView();
            }

        }

        /// <summary>
        /// Поле для хранение команды  для обновления статуса заметки
        /// </summary>
        private RelayCommand _noteStatusUpdateCommand;

        /// <summary>
        /// Свойство для обращения к команде для обновления статуса заметки
        /// </summary>        
        public RelayCommand NoteStatusUpdateCommand =>
            _noteStatusUpdateCommand ??= new RelayCommand(obj => { NoteStatusUpdate(); });

        /// <summary>
        /// Метод для обновления статуса заметки
        /// </summary>
        private void NoteStatusUpdate()
        {
            _model.NoteStatusUpdate();
            UpdateView();
        }

        /// <summary>
        /// Метод для конвертация NoteItemViewModel  в  Note
        /// </summary>
        /// <param name="notesVm"> Коллекция view models заметок</param>
        /// <returns></returns>
        private IEnumerable<Note> ConvertNoteItemViewModelToNote(IEnumerable<NoteItemViewModel> notesVm)
        {
            var notes = new List<Note>();
            foreach (var item in notesVm)
            {
                notes.Add(item.Note);
            }
            return notes;
        }
        #endregion

        #region calendar

        /// <summary>
        /// Текущее время
        /// </summary>
        public string CurrentTime => DateTime.Now.ToString("HH:mm:ss");

        /// <summary>
        /// Коллекция специальных дней 
        /// </summary>
        public ObservableCollection<SpecialDateViewModel> SpecialDatesCollection => _model.SpecialDateCollection;

        #endregion

        #region update view

        /// <summary>
        ///  Метод для автоматического обновления графических элементов раз в 250 миллисекунд
        /// </summary>
        /// <param name="obj"></param>
        private void AutoUpdateView(object obj)
        {
            OnPropertyChanged(nameof(CurrentTime));
        }

        /// <summary>
        /// Метод для обновления графический элементов  вручную.
        /// </summary>
        private void UpdateView()
        {
            OnPropertyChanged(nameof(NoteDescription));
            OnPropertyChanged(nameof(IncompletedNotesCollection));
            OnPropertyChanged(nameof(CompletedNotesCollection));
            OnPropertyChanged(nameof(ShowCompletedCollection));
        }

        #endregion

        #region view navigation

        private RelayCommand _showHolidayViewCommand;

        public RelayCommand ShowHolidayViewCommand => _showHolidayViewCommand ??= new RelayCommand(obj =>
        {
            ShowHolidayViewAsync(_model.CloneRepository(), _mainWindow);
        });

        private void ShowHolidayViewAsync(IMainRepository repos, Window mainWindow)
        {
            _viewService.ShowHolidayWindow(repos, mainWindow);
        }

        private RelayCommand _showPhoneBookViewCommand;

        public RelayCommand ShowPhoneBookViewCommand=> _showPhoneBookViewCommand??= new RelayCommand(obj =>
        {
            ShowPhoneBookViewAsync(_model.CloneRepository(), _mainWindow);
        } );

        private void ShowPhoneBookViewAsync(IMainRepository repos, Window mainWindow)
        {
            _viewService.ShowPhoneBook(repos, mainWindow);
        }

        #endregion

        #region property changed

        /// <summary>
        ///  Событие для подписки на обновления свойств
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Метод для оповещения всех подписчиков
        /// </summary>
        /// <param name="propertyName"></param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
