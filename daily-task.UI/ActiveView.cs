using Caliburn.Micro;
using daily_task.UI.Infrastructure;
using daily_task.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (Parent == null) throw new InvalidOperationException("Parent não foi inicializado.");

            var viewModel = (T)DependencyResolver.CreateInstance(typeof(T), args);
            await Parent.ActivateItemAsync(viewModel);
        }
    }
}
