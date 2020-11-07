using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DataStorage.Source.Entity;
using DataStorage.Source.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Remembrall.Source.ViewModel;

namespace Remembrall.Source.Model
{
    internal class AppModel : IDisposable
    {
        #region notify event
        private delegate void UpdateNotesHandler();
        private event UpdateNotesHandler _updateNotesEvent;
        #endregion
        private readonly IMainRepository _repository;
        private ObservableCollection<NoteItemViewModel> _incompletedIncompletedNotesCollection;
        private ObservableCollection<NoteItemViewModel> _completedNotesCollection;
        private ObservableCollection<SpecialDateViewModel> _specialDateCollection;

        public AppModel()
        {
            _repository = new MainSqlRepository();
            _incompletedIncompletedNotesCollection = MakeIncompletedNotesCollection(_repository);
            _completedNotesCollection = MakeCompletedNotesCollection(_repository);
            _specialDateCollection = MakeSpecialDateCollection(_repository);
            _updateNotesEvent += UpdateNotesCollection;
        }

        #region calendar

        public ObservableCollection<SpecialDateViewModel> SpecialDateCollection => _specialDateCollection;


        private ObservableCollection<SpecialDateViewModel> MakeSpecialDateCollection(IMainRepository _repository)
        {
            var currentMonth = DateTime.Now.Month;
            var dates = _repository.SpecialDateRepository.Find(item => item.Month == currentMonth)
                .Select(date => new SpecialDateViewModel(date)).ToList();
            return new ObservableCollection<SpecialDateViewModel>(dates);
        }

        #endregion

        #region note

        /// <summary>
        /// Коллекция нвыполненных заметок
        /// </summary>
        public ObservableCollection<NoteItemViewModel> IncompletedNotesCollection
        {
            get => _incompletedIncompletedNotesCollection;
        }

        /// <summary>
        /// Коллекция выполненых заметок
        /// </summary>
        public ObservableCollection<NoteItemViewModel> CompletedNotesCollection
        {
            get => _completedNotesCollection;
        }

        /// <summary>
        /// Добавление новых записок в БД
        /// </summary>
        /// <param name="noteDescription"></param>
        public void AddNote(string noteDescription)
        {
            var newNote = new Note
            {
                Description = noteDescription,
                IsDone = false
            };
            _repository.NotesRepository.Add(newNote);
            _repository.SaveChanges();
            _updateNotesEvent?.Invoke();
        }

        /// <summary>
        /// Удаление  коллекций записок из БД
        /// </summary>
        /// <param name="notes"></param>
        public void RemoveNotes(IEnumerable<Note> notes)
        {
            _repository.NotesRepository.DeleteRange(notes);
            _repository.SaveChanges();
            _updateNotesEvent?.Invoke();
        }

        /// <summary>
        /// Удаление определенную записку из БД
        /// </summary>
        /// <param name="note"></param>
        public void RemoveNote(Note note)
        {
            _repository.NotesRepository.Delete(note);
            _repository.SaveChanges();
            _updateNotesEvent?.Invoke();
        }

        /// <summary>
        ///  Метод для обновления статуса заметки
        /// </summary>
        public void NoteStatusUpdate()
        {
            _repository.SaveChanges();
            _updateNotesEvent?.Invoke();
        }

        #endregion

        #region private methods

        private void UpdateNotesCollection()
        {
            _incompletedIncompletedNotesCollection = null;
            _completedNotesCollection = null;
            _incompletedIncompletedNotesCollection = MakeIncompletedNotesCollection(_repository);
            _completedNotesCollection = MakeCompletedNotesCollection(_repository);
        }

        private ObservableCollection<NoteItemViewModel> MakeIncompletedNotesCollection(IMainRepository _repository)
        {
            var notes = _repository.NotesRepository.GetAllItem()
                .Where(n => n.IsDone == false)
                .Select(x => new NoteItemViewModel(x));
            return new ObservableCollection<NoteItemViewModel>(notes);
        }

        private ObservableCollection<NoteItemViewModel> MakeCompletedNotesCollection(IMainRepository _repository)
        {
            var notes = _repository.NotesRepository.GetAllItem()
                .Where(n => n.IsDone == true)
                .Select(n => new NoteItemViewModel(n));
            return new ObservableCollection<NoteItemViewModel>(notes);
        }

        #endregion

        #region clone repositpry

        /// <summary>
        /// Клонирования репозитория для БД для отдачи  в другие модели.
        /// </summary>
        /// <returns></returns>
        public IMainRepository CloneRepository()
        {
            return (IMainRepository)_repository.Clone();
        }

        #endregion

        #region dispose
        public void Dispose()
        {
            _repository?.Dispose();
            _updateNotesEvent -= UpdateNotesCollection;
        }
        #endregion
    }
}
