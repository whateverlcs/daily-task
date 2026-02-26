using Caliburn.Micro;
using daily_task.Application.Models;
using daily_task.Application.UseCases.Task.Delete;
using daily_task.Application.UseCases.Task.GetAllTasks;
using ToastNotifications;
using ToastNotifications.Messages;

namespace daily_task.UI.ViewModels
{
    public class IndexViewModel : Screen
    {
        private bool _loading;

        public bool Loading
        {
            get { return _loading; }
            set
            {
                _loading = value;
                NotifyOfPropertyChange(() => Loading);
            }
        }

        private bool _showMessageWithNoTasks;

        public bool ShowMessageWithNoTasks
        {
            get { return _showMessageWithNoTasks; }
            set
            {
                _showMessageWithNoTasks = value;
                NotifyOfPropertyChange(() => ShowMessageWithNoTasks);
            }
        }

        private string _totalGold = string.Empty;

        public string TotalGold
        {
            get { return _totalGold; }
            set
            {
                _totalGold = value;
                NotifyOfPropertyChange(() => TotalGold);
            }
        }

        public BindableCollection<TaskDisplayModel> Tasks { get; private set; } = new BindableCollection<TaskDisplayModel>();

        private Notifier _notifier;

        private readonly IGetAllTasksUseCase _getAllTasksUseCase;

        private readonly IDeleteTaskUseCase _deleteTaskUseCase;

        public IndexViewModel(IGetAllTasksUseCase getAllTasksUseCase, IDeleteTaskUseCase deleteTaskUseCase, Notifier notifier)
        {
            _getAllTasksUseCase = getAllTasksUseCase;
            _deleteTaskUseCase = deleteTaskUseCase;
            _notifier = notifier;
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            Loading = true;

            _ = LoadTasksAsync();
        }

        private async System.Threading.Tasks.Task LoadTasksAsync()
        {
            var tasks = await _getAllTasksUseCase.Execute();

            Tasks.Clear();
            Tasks.AddRange(tasks);

            ShowMessageWithNoTasks = Tasks.Count == 0;
            Loading = false;
        }

        public void TaskCompleted()
        {
        }

        public void EditTask(int id) => _ = ActiveView.OpenItem<EditTaskViewModel>(id);

        public void DeleteTask(int id)
        {
            Loading = true;

            _ = DeleteTaskAsync(id);
        }

        public async System.Threading.Tasks.Task DeleteTaskAsync(int id)
        {
            bool success = await _deleteTaskUseCase.Execute(id);

            if (success)
            {
                _ = LoadTasksAsync();
                ShowMessageFlashAsync("Success", ["Task deleted successfully!"]);
            }
            else
            {
                ShowMessageFlashAsync("Error", ["An error occurred while deleting the task."]);
            }
        }

        public void AddTask() => _ = ActiveView.OpenItem<CreateTaskViewModel>();

        public void ShowMessageFlashAsync(string messageType, List<string> messages)
        {
            foreach (var message in messages)
            {
                if (messageType == "Info")
                {
                    _notifier.ShowInformation(message);
                }
                else if (messageType == "Success")
                {
                    _notifier.ShowSuccess(message);
                }
                else if (messageType == "Error")
                {
                    _notifier.ShowError(message);
                }
            }
        }
    }
}