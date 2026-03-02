using Caliburn.Micro;
using daily_task.Application.Helpers;
using daily_task.Application.Models;
using daily_task.Application.UseCases.Profile.GetProfile;
using daily_task.Application.UseCases.Profile.Update;
using daily_task.Application.UseCases.Rank.GetAllRanks;
using daily_task.Application.UseCases.Task.GetAllTasks;
using daily_task.Application.UseCases.Task.GetById;
using daily_task.Application.UseCases.Task.Update;
using daily_task.Domain.Enums;
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
        private readonly IGetTaskByIdUseCase _getTaskByIdUseCase;
        private readonly IUpdateTaskUseCase _updateTaskUseCase;
        private readonly IGetProfileUseCase _getProfileUseCase;
        private readonly IUpdateProfileUseCase _updateProfileUseCase;
        private readonly IGetAllRanksUseCase _getAllRanksUseCase;

        public IndexViewModel(
            IGetAllTasksUseCase getAllTasksUseCase,
            IGetTaskByIdUseCase getTaskByIdUseCase,
            IUpdateTaskUseCase updateTaskUseCase,
            IGetProfileUseCase getProfileUseCase,
            IUpdateProfileUseCase updateProfileUseCase,
            IGetAllRanksUseCase getAllRanksUseCase,
            Notifier notifier)
        {
            _getAllTasksUseCase = getAllTasksUseCase;
            _getTaskByIdUseCase = getTaskByIdUseCase;
            _updateTaskUseCase = updateTaskUseCase;
            _getProfileUseCase = getProfileUseCase;
            _updateProfileUseCase = updateProfileUseCase;
            _getAllRanksUseCase = getAllRanksUseCase;
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
            var profile = await _getProfileUseCase.Execute();

            Tasks.Clear();
            Tasks.AddRange(tasks);

            ShowMessageWithNoTasks = Tasks.Count == 0;
            Loading = false;
            TotalGold = profile.GoldBalance + "g";
        }

        public void TaskCompleted(int id)
        {
            _ = TaskCompletedAsync(id);
        }

        public async System.Threading.Tasks.Task TaskCompletedAsync(int id)
        {
            var task = await _getTaskByIdUseCase.Execute(id);

            var success = await _updateTaskUseCase.Execute(id, new NewTask
            {
                Name = task.Name,
                Description = task.Description,
                Priority = task.Priority.ToPriorityEnum(),
                Status = Status.Active,
                Gold = task.Gold,
                Active = false
            });

            if (success)
            {
                await UpdateProfileAsync(task.Gold);

                ShowMessageFlashAsync("Success", ["Task completed successfully!"]);
                await LoadTasksAsync();
            }
            else
            {
                ShowMessageFlashAsync("Error", ["Failed to complete the task."]);
            }
        }

        public async System.Threading.Tasks.Task UpdateProfileAsync(string gold)
        {
            var profile = await _getProfileUseCase.Execute();
            var ranks = await _getAllRanksUseCase.Execute();

            profile.TasksCompleted += 1;

            int goldToAdd = int.Parse(gold.Replace("g", ""));

            profile.GoldEarned += goldToAdd;
            profile.GoldBalance += goldToAdd;
            profile.RankId = ranks.LastOrDefault(r => profile.TasksCompleted >= r.TaskGoal)?.Id ?? profile.RankId;

            await _updateProfileUseCase.Execute(profile);
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