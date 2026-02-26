using Caliburn.Micro;
using daily_task.Application.Helpers;
using daily_task.Application.Models;
using daily_task.Application.UseCases.Task.Register;
using daily_task.Domain.Enums;
using daily_task.Exceptions.ExceptionsBase;
using System.Collections.ObjectModel;
using ToastNotifications;
using ToastNotifications.Messages;

namespace daily_task.UI.ViewModels
{
    public class CreateTaskViewModel : Screen
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

        private Notifier _notifier;

        private readonly IRegisterTaskUseCase _registerTaskUseCase;

        public CreateTaskViewModel(IRegisterTaskUseCase registerTaskUseCase, Notifier notifier)
        {
            _registerTaskUseCase = registerTaskUseCase;
            _notifier = notifier;
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            ListPriority.Clear();
            ListPriority = new ObservableCollection<PriorityDisplayModel>(PriorityHelper.GetPriorities());
        }

        public void CreateTask()
        {
            EnableFields = false;
            Loading = true;
            _ = CreateTasksAsync();
        }

        private async System.Threading.Tasks.Task CreateTasksAsync()
        {
            try
            {
                var sucess = await _registerTaskUseCase.Execute(new NewTask
                {
                    Name = Title,
                    Description = Description,
                    Priority = SelectedPriority != null ? (Priority)SelectedPriority.Id : new Priority(),
                    Status = Status.Active,
                    Gold = SelectedPriority != null ? $"{((Priority)SelectedPriority.Id).GetRandomGold()}g" : string.Empty
                });

                if (sucess)
                {
                    ShowMessageFlashAsync("Success", ["Task created successfully!"]);
                    ClearFields();
                }
                else
                {
                    ShowMessageFlashAsync("Error", ["An error occurred while creating the task."]);
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

        public void ClearFields()
        {
            Title = string.Empty;
            Description = string.Empty;
            SelectedPriority = null!;
        }

        public void ReturnToHome() => _ = ActiveView.OpenItem<IndexViewModel>();
    }
}