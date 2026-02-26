using Caliburn.Micro;
using daily_task.Application.Helpers;
using daily_task.Application.Models;
using daily_task.Application.UseCases.Task.GetById;
using daily_task.Application.UseCases.Task.Update;
using daily_task.Domain.Enums;
using daily_task.Exceptions.ExceptionsBase;
using System.Collections.ObjectModel;
using ToastNotifications;
using ToastNotifications.Messages;

namespace daily_task.UI.ViewModels
{
    public class EditTaskViewModel : Screen
    {
        private bool _enableFields = true;

        public bool EnableFields
        {
            get { return _enableFields; }
            set
            {
                _enableFields = value;
                NotifyOfPropertyChange(() => EnableFields);
            }
        }

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

        private string _title = string.Empty;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        private PriorityDisplayModel _selectedPriority = new PriorityDisplayModel();

        public PriorityDisplayModel SelectedPriority
        {
            get { return _selectedPriority; }
            set
            {
                _selectedPriority = value;
                NotifyOfPropertyChange(() => SelectedPriority);
            }
        }

        private ObservableCollection<PriorityDisplayModel> _listPriority = new ObservableCollection<PriorityDisplayModel>();

        public ObservableCollection<PriorityDisplayModel> ListPriority
        {
            get { return _listPriority; }
            set
            {
                _listPriority = value;
                NotifyOfPropertyChange(() => ListPriority);
            }
        }

        private string _description = string.Empty;

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                NotifyOfPropertyChange(() => Description);
            }
        }

        private string _gold = string.Empty;

        public string Gold
        {
            get { return _gold; }
            set
            {
                _gold = value;
                NotifyOfPropertyChange(() => Gold);
            }
        }

        private string _priorityOriginal = string.Empty;

        public string PriorityOriginal
        {
            get { return _priorityOriginal; }
            set
            {
                _priorityOriginal = value;
                NotifyOfPropertyChange(() => PriorityOriginal);
            }
        }

        private Notifier _notifier;

        private readonly IUpdateTaskUseCase _updateTaskUseCase;

        private readonly IGetTaskByIdUseCase _getTaskByIdUseCase;

        private int _taskId;

        public EditTaskViewModel(int taskId, IUpdateTaskUseCase updateTaskUseCase, IGetTaskByIdUseCase getTaskByIdUseCase, Notifier notifier)
        {
            _taskId = taskId;
            _updateTaskUseCase = updateTaskUseCase;
            _getTaskByIdUseCase = getTaskByIdUseCase;
            _notifier = notifier;
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            _ = LoadFields();
        }

        public async System.Threading.Tasks.Task LoadFields()
        {
            ListPriority.Clear();
            ListPriority = new ObservableCollection<PriorityDisplayModel>(PriorityHelper.GetPriorities());

            var task = await _getTaskByIdUseCase.Execute(_taskId);

            if (task != null)
            {
                Title = task.Name;
                Description = task.Description;
                SelectedPriority = ListPriority.FirstOrDefault(p => p.Priority == task.Priority) ?? new PriorityDisplayModel();
                PriorityOriginal = SelectedPriority.Priority;
                Gold = task.Gold;
            }
        }

        public void EditTask()
        {
            EnableFields = false;
            Loading = true;
            _ = EditTasksAsync();
        }

        private async System.Threading.Tasks.Task EditTasksAsync()
        {
            try
            {
                var sucess = await _updateTaskUseCase.Execute(_taskId, new NewTask
                {
                    Name = Title,
                    Description = Description,
                    Priority = (Priority)SelectedPriority.Id,
                    Status = Status.Active,
                    Gold = PriorityOriginal != SelectedPriority.Priority ? $"{((Priority)SelectedPriority.Id).GetRandomGold()}g" : Gold
                });

                if (sucess)
                {
                    ShowMessageFlashAsync("Success", ["Task updated successfully!"]);
                }
                else
                {
                    ShowMessageFlashAsync("Error", ["An error occurred while updating the task."]);
                }
            }
            catch (ErrorOnValidationException ex)
            {
                ShowMessageFlashAsync("Error", [.. ex.GetErrorMessages()]);
            }
            catch (Exception)
            {
                ShowMessageFlashAsync("Error", ["An unexpected error occurred."]);
            }
            finally
            {
                EnableFields = true;
                Loading = false;
            }
        }

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

        public void ReturnToHome() => _ = ActiveView.OpenItem<IndexViewModel>();
    }
}