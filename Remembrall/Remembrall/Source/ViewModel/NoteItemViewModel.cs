using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using DataStorage.Source.Entity;

namespace Remembrall.Source.ViewModel
{
    public class NoteItemViewModel
    {
        private readonly Note _note;

        public NoteItemViewModel(Note note)
        {
            _note = note;
        }

        public string Description => _note.Description;

        public bool IsDone
        {
            get => _note.IsDone;
            set => _note.IsDone = value;
        }

        public Note Note => _note;
    }
}
