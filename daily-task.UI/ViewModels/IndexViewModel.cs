using Caliburn.Micro;
using daily_task.Application.Models;
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

        public IndexViewModel(IGetAllTasksUseCase getAllTasksUseCase, Notifier notifier)
        {
            _getAllTasksUseCase = getAllTasksUseCase;
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