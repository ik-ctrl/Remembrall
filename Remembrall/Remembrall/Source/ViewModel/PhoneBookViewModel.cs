using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using DataStorage.Source.Entity;
using DataStorage.Source.Repository;
using Remembrall.Annotations;
using Remembrall.Source.Infrastructure.Interfaces;
using Remembrall.Source.Model;

namespace Remembrall.Source.ViewModel
{
    public class PhoneBookViewModel : INotifyPropertyChanged
    {
        private PhoneBookModel _model;
        private string _personName;
        private string _personSurname;
        private List<string> _phones;
        private List<string> _emails;
        private RelationshipEnumViewModel _selectedRelation;


        public PhoneBookViewModel(IMainRepository repository)
        {
            _model = new PhoneBookModel(repository);
            IntializeValue();
            Phones.Add("+79506239278");
            Phones.CollectionChanged += PhonesCollectionChanged;
        }


        public string PersonName
        {
            get => _personName;
            set
            {
                _personName = value;
                OnPropertyChanged(nameof(PersonName));
            }
        }

        public string PersonSurname
        {
            get => _personSurname;
            set
            {
                _personSurname = value;
                OnPropertyChanged(nameof(PersonSurname));
            }
        }

        public RelationshipEnumViewModel SelectedRelation
        {
            get => _selectedRelation;
            set
            {
                if (_selectedRelation == value)
                    return;
                _selectedRelation = value;
                OnPropertyChanged(nameof(SelectedRelation));
            }
        }


        public ObservableCollection<string> Phones { get; set; }
        private void PhonesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        var item = e.NewItems;
                        _phones.Add(e.NewItems.ToString());
                        break;
                    }
            }
        }


        private void IntializeValue()
        {
            Phones= new ObservableCollection<string>();
            //Email= new ObservableCollection<>();
        }

        #region notification
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
