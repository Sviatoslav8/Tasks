using BLL.Configuration;
using BLL.interfaces;
using BLL.Models;
using BLL.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Data;
using System.Windows;

namespace E_Framework_Core
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider serviceProvider;
        private string _connectionstring;
        public App()
        {
            _connectionstring = ConfigurationManager.ConnectionStrings["Register"].ConnectionString;
            var service = new ServiceCollection();
            ConfigurationService(service);
            serviceProvider = service.BuildServiceProvider();
        }
        private void ConfigurationService(ServiceCollection service)
        {
            service.AddTransient(typeof(IsServices<People>), typeof(PeopleService));
            service.AddTransient(typeof(MainWindow));
            ConfigurationBLL.ConfigurationServiceCollection(service, _connectionstring);
        }
        public void OnStartUp(object sender, StartupEventArgs arg)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }

}
