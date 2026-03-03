using daily_task.Exceptions.ExceptionsBase;
using System.Windows;

namespace daily_task.UI.Filters
{
    public static class ExceptionManager
    {
        public static void HandleException(Exception ex)
        {
            if (ex is DailyTaskException schoolException)
            {
                var messages = schoolException.GetErrorMessages();
                MessageBox.Show(string.Join(Environment.NewLine, messages), "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                MessageBox.Show("Ocorreu um erro inesperado. Tente novamente mais tarde.", "Erro Fatal", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}