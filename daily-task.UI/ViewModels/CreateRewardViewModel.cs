using Caliburn.Micro;
using daily_task.Application.Models;
using daily_task.Application.UseCases.Reward.Register;
using daily_task.Exceptions.ExceptionsBase;
using ToastNotifications;
using ToastNotifications.Messages;

namespace daily_task.UI.ViewModels
{
    public class CreateRewardViewModel : Screen
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

        private string _reward = string.Empty;

        public string Reward
        {
            get { return _reward; }
            set
            {
                _reward = value;
                NotifyOfPropertyChange(() => Reward);
            }
        }

        private string _goldCost = string.Empty;

        public string GoldCost
        {
            get { return _goldCost; }
            set
            {
                _goldCost = value;
                NotifyOfPropertyChange(() => GoldCost);
            }
        }

        private Notifier _notifier;

        private readonly IRegisterRewardUseCase _registerRewardUseCase;

        public CreateRewardViewModel(
            IRegisterRewardUseCase registerRewardUseCase,
            Notifier notifier)
        {
            _registerRewardUseCase = registerRewardUseCase;
            _notifier = notifier;
        }

        public void CreateReward()
        {
            EnableFields = false;
            Loading = true;
            _ = CreateRewardAsync();
        }

        private async System.Threading.Tasks.Task CreateRewardAsync()
        {
            try
            {
                var sucess = await _registerRewardUseCase.Execute(new RewardDisplayModel
                {
                    Name = Reward,
                    Gold = $"{GoldCost}g",
                    Active = true
                });

                if (sucess)
                {
                    ShowMessageFlashAsync("Success", ["Reward created successfully!"]);
                    ClearFields();
                }
                else
                {
                    ShowMessageFlashAsync("Error", ["An error occurred while creating the reward."]);
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
            Reward = string.Empty;
            GoldCost = string.Empty;
        }

        public void ReturnToRewards() => _ = ActiveView.OpenItem<RewardViewModel>();
    }
}