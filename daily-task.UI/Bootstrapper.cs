using Caliburn.Micro;
using daily_task.Infrastructure;
using daily_task.Infrastructure.Extensions;
using daily_task.Infrastructure.Migrations;
using daily_task.UI.Infrastructure;
using daily_task.UI.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace daily_task.UI
{
    public class Bootstrapper : BootstrapperBase
    {
        private readonly SimpleContainer _container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            // Carrega o JSON
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();


            // Setup da Infra
            var services = new ServiceCollection();
            services.AddInfrastructure(configuration);

            // Provider para as migrations
            var serviceProvider = services.BuildServiceProvider();

            // Registro do container do Caliburn
            _container.Instance<IConfiguration>(configuration);
            _container.Instance<IServiceProvider>(serviceProvider);

            // Registros do Caliburn (WindowManager, ViewModels, etc)
            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventAggregator, EventAggregator>();
            _container.PerRequest<ShellViewModel>();
            _container.PerRequest<IndexViewModel>();

            // Registro do ServiceProvider
            DependencyResolver.SetContainer(_container);
        }

        protected override async void OnStartup(object sender, StartupEventArgs e)
        {
            MigrateDatabase();

            await DisplayRootViewForAsync<ShellViewModel>();
        }

        private void MigrateDatabase()
        {
            var configuration = _container.GetInstance<IConfiguration>();

            if (configuration.IsUnitTestEnviroment()) return;

            var databaseType = configuration.DatabaseType();
            var connectionString = configuration.ConnetionString();

            var serviceProvider = _container.GetInstance<IServiceProvider>();

            DatabaseMigration.Migrate(databaseType, connectionString, serviceProvider);
        }

        protected override object GetInstance(Type service, string key) => _container.GetInstance(service, key);
        protected override IEnumerable<object> GetAllInstances(Type service) => _container.GetAllInstances(service);
        protected override void BuildUp(object instance) => _container.BuildUp(instance);
    }
}
