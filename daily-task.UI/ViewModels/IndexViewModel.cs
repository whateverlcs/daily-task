using Caliburn.Micro;
using daily_task.Application.Models;
using daily_task.Application.UseCases.Task.GetAllTasks;

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

        private readonly IGetAllTasksUseCase _getAllTasksUseCase;

        public IndexViewModel(IGetAllTasksUseCase getAllTasksUseCase)
        {
            _getAllTasksUseCase = getAllTasksUseCase;
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

        public void AddTask()
        {
        }

        public void EditTask(int id)
        {
        }

        public void DeleteTask(int id)
        {
        }
    }
}