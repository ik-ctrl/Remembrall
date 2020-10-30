using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Remembrall.Annotations;
using Remembrall.Source.Infrastructure;
using Remembrall.Source.Model;

namespace Remembrall.Source.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly AppModel model;
        private string _noteDescription;

        public MainViewModel()
        {
            model = new AppModel();
            _noteDescription = string.Empty;
        }



        #region notes

        //todo: xyeta kakyto
        private bool IsEmptyDescription =>
            string.IsNullOrEmpty(_noteDescription.Trim());

        public bool ShowCompletedCollection =>
            model.CompletedNotesCollection.Count != 0;

        public string NoteDescription
        {
            get => _noteDescription;
            set => _noteDescription = value;
        }

        public ObservableCollection<NoteItemViewModel> IncompletedNotesCollection
        {
            get => model.IncompletedNotesCollection;
        }

        public ObservableCollection<NoteItemViewModel> CompletedNotesCollection
        {
            get => model.CompletedNotesCollection;
        }

        //todo: add func  for  command
        private RelayCommand _addNoteCommand;
        public RelayCommand AddNoteCommand => _addNoteCommand ?? new RelayCommand(obj =>
       {
           AddNote();
       });

        private RelayCommand _removeIncompletedNotesCommand;
        public RelayCommand RemoveIncompletedNotesCommand => _removeIncompletedNotesCommand
                                                             ??= new RelayCommand(obj => { RemoveIncompletedNotes(); },
                                                                 obj => { return true; });
        private RelayCommand _removeCompletedNotesCommand;
        public RelayCommand RemoveCompletedNotesCommand => _removeCompletedNotesCommand
                                                           ??= new RelayCommand(obj => { RemoveCompletedNotes(); },
                                                               obj => { return true; });
        //todo: хуета не работает . биндинг на верхнии уровни не работает.
        private RelayCommand _removeSpecialNoteCommand;
        public RelayCommand RemoveSpecialNoteCommand => _removeSpecialNoteCommand
                                                        ??(_removeSpecialNoteCommand = new RelayCommand(obj => { RemoveSpecialNote(obj); }));


        private void AddNote()
        {
            model.AddNote(_noteDescription.Trim());
            _noteDescription = string.Empty;
            UpdateNoteView();
        }

        private void RemoveIncompletedNotes()
        {
            MessageBox.Show("Udalyaem vse pisma zavershennie");
        }

        private void RemoveCompletedNotes()
        {
            MessageBox.Show("Udalyaem vse pisma zavershennie");
        }

        private void RemoveSpecialNote(object obj)
        {
            MessageBox.Show("удаляем одну заметку");
        }

        private void UpdateNoteView()
        {
            OnPropertyChanged(nameof(NoteDescription));
            OnPropertyChanged(nameof(IncompletedNotesCollection));
            OnPropertyChanged(nameof(CompletedNotesCollection));
            OnPropertyChanged(nameof(ShowCompletedCollection));
        }
        #endregion


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
