using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using DataStorage.Source.Entity;
using Remembrall.Annotations;
using Remembrall.Source.Infrastructure;
using Remembrall.Source.Model;

namespace Remembrall.Source.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly AppModel _model;
        private string _noteDescription;

        public MainViewModel()
        {
            _model = new AppModel();
            _noteDescription = string.Empty;
        }

        #region notes view

        public bool ShowCompletedCollection => _model.CompletedNotesCollection.Count != 0;

        public string NoteDescription
        {
            get => _noteDescription;
            set => _noteDescription = value;
        }

        public ObservableCollection<NoteItemViewModel> IncompletedNotesCollection
        {
            get => _model.IncompletedNotesCollection;
        }

        public ObservableCollection<NoteItemViewModel> CompletedNotesCollection
        {
            get => _model.CompletedNotesCollection;
        }

        private RelayCommand _addNoteCommand;

        public RelayCommand AddNoteCommand => _addNoteCommand ?? new RelayCommand(obj =>
       {
           AddNote();
       });

        private void AddNote()
        {
            if (string.IsNullOrEmpty(_noteDescription.Trim())) 
                return;
            _model.AddNote(_noteDescription.Trim());
            _noteDescription = string.Empty;
            UpdateNoteView();
        }

        private RelayCommand _removeIncompletedNotesCommand;

        public RelayCommand RemoveIncompletedNotesCommand => 
            _removeIncompletedNotesCommand ??= new RelayCommand(obj => { RemoveIncompletedNotes(); }, 
                                    obj => { return IncompletedNotesCollection.Count != 0;});

        private void RemoveIncompletedNotes()
        {
            var notesVm = IncompletedNotesCollection.ToList();
            _model.RemoveNotes(ConvertNoteItemViewModelToNote(notesVm));
            UpdateNoteView();
        }

        private RelayCommand _removeCompletedNotesCommand;

        public RelayCommand RemoveCompletedNotesCommand => 
            _removeCompletedNotesCommand  ??= new RelayCommand(obj => { RemoveCompletedNotes(); }, 
                obj => { return CompletedNotesCollection.Count != 0;});

        private void RemoveCompletedNotes()
        {
            var notesVm = CompletedNotesCollection.ToList();
            _model.RemoveNotes(ConvertNoteItemViewModelToNote(notesVm));
            UpdateNoteView();
        }

        private RelayCommand _removeSpecialNoteCommand;

        public RelayCommand RemoveSpecialNoteCommand =>
            _removeSpecialNoteCommand ??= new RelayCommand(obj => { RemoveSpecialNote(obj); });

        private void RemoveSpecialNote(object obj)
        {
            if (obj is NoteItemViewModel note)
            {
                _model.RemoveNote(note.Note);
                UpdateNoteView();
            }

        }

        private RelayCommand _noteStatusUpdateCommand;

        public RelayCommand NoteStatusUpdateCommand =>
            _noteStatusUpdateCommand ??= new RelayCommand(obj => { NoteStatusUpdate(); });

        private void NoteStatusUpdate()
        {
            _model.NoteStatusUpdate();
            UpdateNoteView();
        }

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

        private void UpdateNoteView()
        {
            OnPropertyChanged(nameof(NoteDescription));
            OnPropertyChanged(nameof(IncompletedNotesCollection));
            OnPropertyChanged(nameof(CompletedNotesCollection));
            OnPropertyChanged(nameof(ShowCompletedCollection));
        }
        #region property changed
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
