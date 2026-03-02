using Caliburn.Micro;
using daily_task.Application.Models;
using daily_task.Application.UseCases.Reward.GetById;
using daily_task.Application.UseCases.Reward.Update;
using daily_task.Exceptions.ExceptionsBase;
using ToastNotifications;
using ToastNotifications.Messages;

namespace daily_task.UI.ViewModels
{
    public class EditRewardViewModel : Screen
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
        private readonly IUpdateRewardUseCase _updateRewardUseCase;
        private readonly IGetRewardByIdUseCase _getRewardByIdUseCase;
        private int _rewardId;

        public EditRewardViewModel(
            int rewardId,
            IUpdateRewardUseCase updateRewardUseCase,
            IGetRewardByIdUseCase getRewardByIdUseCase,
            Notifier notifier)
        {
            _rewardId = rewardId;
            _updateRewardUseCase = updateRewardUseCase;
            _getRewardByIdUseCase = getRewardByIdUseCase;
            _notifier = notifier;
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            _ = LoadFields();
        }

        public async System.Threading.Tasks.Task LoadFields()
        {
            var reward = await _getRewardByIdUseCase.Execute(_rewardId);

            if (reward != null)
            {
                Reward = reward.Name;
                GoldCost = reward.Gold.Replace("g", "");
            }
        }

        public void EditReward()
        {
            EnableFields = false;
            Loading = true;
            _ = EditRewardAsync();
        }

        private async System.Threading.Tasks.Task EditRewardAsync()
        {
            try
            {
                var sucess = await _updateRewardUseCase.Execute(_rewardId, new RewardDisplayModel
                {
                    Id = _rewardId,
                    Name = Reward,
                    Gold = $"{GoldCost}g",
                    Active = true
                });

                if (sucess)
                {
                    ShowMessageFlashAsync("Success", ["Reward updated successfully!"]);
                }
                else
                {
                    ShowMessageFlashAsync("Error", ["An error occurred while updating the reward."]);
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

        public void ReturnToRewards() => _ = ActiveView.OpenItem<RewardViewModel>();
    }
}