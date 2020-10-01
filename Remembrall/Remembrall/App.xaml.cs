using System;
using System.Threading;
using System.Windows;
using DataStorage.Source.Repository;

namespace Remembrall
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            IMainRepository mainRepository = new MainSqlRepository();
            CheckConnection(mainRepository);
            DeployMigration(mainRepository);
        }

        private void DeployMigration(IMainRepository main)
        {
            main.DeployMigration();
        }

        private void CheckConnection(IMainRepository main)
        {
            var counter = 0;
            var isConnect = false;

            while (counter < 5)
            {
                isConnect = main.IsConnect();
                if (isConnect) break;
                Thread.Sleep(TimeSpan.FromSeconds(2));
                counter++;
            }

            if (!isConnect)
            {
                MessageBox.Show("Не удалось подключится к базе данных.\nПопробуйте позже...", "Ошибка...", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
            }
        }
    }
}
