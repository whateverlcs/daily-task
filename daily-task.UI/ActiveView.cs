using Caliburn.Micro;
using daily_task.UI.Infrastructure;
using daily_task.UI.ViewModels;

namespace daily_task.UI
{
    public static class ActiveView
    {
        public static ShellViewModel? Parent;

        /// <summary>
        /// Realiza a abertura de um viewmodel através do ShellViewModel
        /// </summary>
        public static async Task OpenItem<T>() where T : IScreen
        {
            if (Parent == null)
                throw new InvalidOperationException("Parent não foi inicializado.");

            var viewModel = DependencyResolver.GetService<T>();
            await Parent.ActivateItemAsync(viewModel);
        }
    }
}