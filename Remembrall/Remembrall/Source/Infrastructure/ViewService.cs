using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using DataStorage.Source.Repository;
using Remembrall.Source.Infrastructure.Interfaces;
using Remembrall.Source.Model;
using Remembrall.Source.View;
using Remembrall.Source.ViewModel;

namespace Remembrall.Source.Infrastructure
{
    public class ViewService : IViewService
    {
        //todo: попробовать Task.Run
        public  void ShowHolidayWindow(IMainRepository repository, Window owner)
        {
            Dispatcher.CurrentDispatcher?.Invoke(() =>
            {
                var window = new HolidayView()
                {
                    DataContext = new HolidayViewModel(repository),
                    Owner = owner
                };
                window.Show();
            });

        }

        //todo: попробовать Task.Run
        public  void ShowPhoneBook(IMainRepository repository, Window owner)
        {
            Dispatcher.CurrentDispatcher?.Invoke(() =>
            {
                var window = new PhoneBookView()
                {
                    DataContext = new PhoneBookViewModel(repository),
                    Owner = owner,
                };
                window.Show();
            });
        }

    }
}
