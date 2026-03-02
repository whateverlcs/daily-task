using Caliburn.Micro;
using daily_task.Application;
using daily_task.Infrastructure;
using daily_task.Infrastructure.Extensions;
using daily_task.Infrastructure.Migrations;
using daily_task.UI.Infrastructure;
using daily_task.UI.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;

namespace daily_task.UI
{
    public class Bootstrapper : BootstrapperBase
    {
        private IServiceProvider _serviceProvider;

        public Bootstrapper() => Initialize();

        protected override void Configure()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var services = new ServiceCollection();

            // Registro de Serviços
            services.AddApplication(configuration);
            services.AddInfrastructure(configuration);

            // Registro de ViewModels e Infra do Caliburn
            services.AddTransient<ShellViewModel>();
            services.AddTransient<IndexViewModel>();
            services.AddTransient<CreateTaskViewModel>();
            services.AddTransient<EditTaskViewModel>();
            services.AddTransient<RewardViewModel>();
            services.AddTransient<CreateRewardViewModel>();
            services.AddSingleton<IWindowManager, WindowManager>();
            services.AddSingleton<IEventAggregator, EventAggregator>();

            // Registro do Notifier para exibir notificações
            services.AddSingleton<Notifier>(sp =>
            {
                return new Notifier(cfg =>
                {
                    cfg.PositionProvider = new WindowPositionProvider(
                        parentWindow: System.Windows.Application.Current.MainWindow,
                        corner: Corner.TopRight,
                        offsetX: 10,
                        offsetY: 10);

                    cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                        notificationLifetime: TimeSpan.FromSeconds(5),
                        maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                    cfg.Dispatcher = System.Windows.Application.Current.Dispatcher;
                });
            });

            // Registra instâncias necessárias para o MigrateDatabase
            services.AddSingleton<IConfiguration>(configuration);

            _serviceProvider = services.BuildServiceProvider();
            DependencyResolver.SetServiceProvider(_serviceProvider);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            MigrateDatabase();
            DisplayRootViewForAsync<ShellViewModel>();
        }

        private void MigrateDatabase()
        {
            var configuration = _serviceProvider.GetRequiredService<IConfiguration>();
            if (configuration.IsUnitTestEnviroment()) return;

            DatabaseMigration.Migrate(
                configuration.DatabaseType(),
                configuration.ConnetionString(),
                _serviceProvider);
        }

        protected override object GetInstance(Type service, string key) =>
            key == null ? _serviceProvider.GetRequiredService(service) : _serviceProvider.GetRequiredKeyedService(service, key);

        protected override IEnumerable<object> GetAllInstances(Type service) =>
            _serviceProvider.GetServices(service);

        protected override void BuildUp(object instance)
        { }
    }
}