using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using DataStorage.Source.Entity;
using DataStorage.Source.Repository;
using Microsoft.EntityFrameworkCore;
using Remembrall.Source.ViewModel;

namespace Remembrall.Source.Model
{
    internal class AppModel : IDisposable
    {
        private delegate void UpdateNotesHandler();

        private event UpdateNotesHandler _updateNotesEvent;



        private readonly IMainRepository _repository;
        private ObservableCollection<NoteItemViewModel> _notesCollection;

        public AppModel()
        {
            _repository = new MainSqlRepository();
            _notesCollection = CreateCollection(_repository);
            _updateNotesEvent += UpdateNotesCollection;
        }


        public ObservableCollection<NoteItemViewModel> NotesCollection
        {
            get => _notesCollection;
        }


        public void AddNote(string noteDescription)
        {
            var newNote = new Note();
            newNote.Description = noteDescription;
            newNote.IsDone = false;
            _repository.NotesRepository.Add(newNote);
            _repository.SaveChanges();
            _updateNotesEvent?.Invoke();

        }


        #region private methods

        private void UpdateNotesCollection()
        {
            _notesCollection = null;
            _notesCollection = CreateCollection(_repository);
        }

        private ObservableCollection<NoteItemViewModel> CreateCollection(IMainRepository _repository)
        {
            var notes = _repository.NotesRepository.GetAllItem().Select(x => new NoteItemViewModel(x));
            return new ObservableCollection<NoteItemViewModel>(notes);
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
