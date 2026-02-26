using Caliburn.Micro;
using daily_task.UI.Infrastructure;
using daily_task.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace daily_task.UI
{
    public static class ActiveView
    {
        public static ShellViewModel? Parent;

        /// <summary>
        /// Realiza a abertura de um viewmodel através do ShellViewModel
        /// </summary>
        public static async Task OpenItem<T>(params object[] args) where T : IScreen
        {
            if (Parent == null)
                throw new InvalidOperationException("Parent não foi inicializado.");

            T viewModel;

            if (args != null && args.Length > 0)
            {
                var serviceProvider = DependencyResolver.GetServiceProvider();
                viewModel = ActivatorUtilities.CreateInstance<T>(serviceProvider, args);
            }
            else
            {
                viewModel = DependencyResolver.GetService<T>();
            }

            await Parent.ActivateItemAsync(viewModel);
        }
    }
}