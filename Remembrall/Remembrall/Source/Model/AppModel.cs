using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using DataStorage.Source.Entity;
using DataStorage.Source.Repository;
using Remembrall.Source.ViewModel;

namespace Remembrall.Source.Model
{
    internal class AppModel
    {
        private readonly IMainRepository _repository;
        

        public AppModel()
        {
            _repository =  new MainSqlRepository();
        }


        public void AddNote(string noteDescription)
        {
            var newNote = new Note();
            newNote.Description = noteDescription;
            newNote.IsDone = false;
            _repository.NotesRepository.Add(newNote);
        }


    }
}
