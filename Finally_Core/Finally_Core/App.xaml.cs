using BLL.configuration;
using BLL.interfaces;
using BLL.models;
using BLL.services;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Finally_Core
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;
        private string _connectionString;
        public App()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["NetworkMusic"].ConnectionString;
            var service = new ServiceCollection();
            ConfigurationService(service);
            _serviceProvider = service.BuildServiceProvider();
        }

        private void ConfigurationService(ServiceCollection service)
        {
            service.AddTransient(typeof(IService<Product>), typeof(ShopService));
            service.AddTransient(typeof(MainWindow));
            ConfigurationBLL.ConfigurationCollection(service, _connectionString);
        }
        public void OnStartUp(object sender, StartupEventArgs arg)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }

}
