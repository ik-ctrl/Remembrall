using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using DataStorage.Source.Entity;
using DataStorage.Source.Repository;
using Remembrall.Source.ViewModel;

namespace Remembrall.Source.Model
{
    internal class AppModel
    {
        private readonly IMainRepository _repository;
        private ObservableCollection<NoteItemViewModel> _notesCollection;

        public AppModel()
        {
            _repository = new MainSqlRepository();
            _notesCollection = CreateCollection(_repository);
        }


        public ObservableCollection<NoteItemViewModel> NotesCollection
        {
            get =>_notesCollection;
        }


        public void AddNote(string noteDescription)
        {
            var newNote = new Note();
            newNote.Description = noteDescription;
            newNote.IsDone = false;
            _repository.NotesRepository.Add(newNote);
            _repository.SaveChanges();
        }


        #region private

        private ObservableCollection<NoteItemViewModel> CreateCollection(IMainRepository _repository)
        {
            var notes = _repository.NotesRepository.GetAllItem().Select(x => new NoteItemViewModel(x));
            return new ObservableCollection<NoteItemViewModel>(notes);
        }

        #endregion


    }
}
