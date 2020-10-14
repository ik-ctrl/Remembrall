using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using DataStorage.Source.Entity;

namespace Remembrall.Source.ViewModel
{
    public class NoteItemViewModel
    {
        private Note _note;

        public NoteItemViewModel(Note note)
        {
            _note = note;
        }

        public string Description => _note.Description;

        public bool IsDone => _note.IsDone;

    }
}
