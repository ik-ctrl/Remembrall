using System;
using System.Threading.Tasks;
using System.Windows;
using DataStorage.Source.Repository;
using Remembrall.Source.Infrastructure.Interfaces;
using Remembrall.Source.Model;
using Remembrall.Source.View;
using Remembrall.Source.ViewModel;

namespace Remembrall.Source.Infrastructure
{
    public class ViewService:IViewService
    {
        public async void ShowHolidayWindowAsync(IMainRepository repository, Window owner)
        {
            var window = new HolidayView()
            {
                DataContext = new HolidayViewModel(repository),
                Owner=owner
            };
            await Task.Run(() => 
                window.Show()
                );
        }

        public async void ShowPhoneBook(IMainRepository repository, Window owner)
        {
            throw new NotImplementedException();
        }

    }
}
