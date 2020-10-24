using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using DataStorage.Source.Repository;
using Remembrall.Annotations;
using Remembrall.Source.Infrastructure;
using Remembrall.Source.Model;

namespace Remembrall.Source.ViewModel
{
    public  class MainViewModel:INotifyPropertyChanged
    {
        private readonly AppModel model;
        private string _noteDescription;
        public MainViewModel()
        {
            model = new AppModel();
            _noteDescription = string.Empty;
        }



        #region notes property

        public bool IsEmptyDescription => string.IsNullOrEmpty(_noteDescription.Trim());

        public string NoteDescription
        {
            get => _noteDescription;
            set => _noteDescription=value;
        }

        public ObservableCollection<NoteItemViewModel> NotesCollection
        {
           // get => model.NoteCollection;
           get => null;
        }

        private RelayCommand _addNoteCommand;

        public ICommand AddNoteCommand => _addNoteCommand ?? new RelayCommand(AddNote, () =>IsEmptyDescription);
        public void AddNote()
        {
            model.AddNote(_noteDescription.Trim());
            _noteDescription = string.Empty;
            OnPropertyChanged(nameof(NoteDescription));
            OnPropertyChanged(nameof(NotesCollection));
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
