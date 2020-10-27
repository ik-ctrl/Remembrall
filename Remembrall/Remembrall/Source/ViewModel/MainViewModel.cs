using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DataStorage.Source.Entity;
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

        public bool VisibilityCompletedCollection =>model.CompletedNotesCollection.Count !=0;



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

        private void AddNote()
        {
            model.AddNote(_noteDescription.Trim());
            _noteDescription = string.Empty;
            OnPropertyChanged(nameof(NoteDescription));
            OnPropertyChanged(nameof(IncompletedNotesCollection));
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
