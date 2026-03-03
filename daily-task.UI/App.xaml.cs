using daily_task.UI.Filters;

namespace daily_task.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public App()
        {
            DispatcherUnhandledException += (sender, e) =>
            {
                ExceptionManager.HandleException(e.Exception);
                e.Handled = true;
            };
        }
    }
}