using Caliburn.Micro;
using daily_task.Application.Models;
using daily_task.Application.UseCases.Profile.GetProfile;
using daily_task.Application.UseCases.Profile.Update;
using daily_task.Application.UseCases.Reward.GetAllRewards;
using daily_task.Application.UseCases.Reward.GetById;
using daily_task.Application.UseCases.Reward.Update;
using ToastNotifications;
using ToastNotifications.Messages;

namespace daily_task.UI.ViewModels
{
    public class RewardViewModel : Screen
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

        private bool _showMessageWithNoRewards;

        public bool ShowMessageWithNoRewards
        {
            get { return _showMessageWithNoRewards; }
            set
            {
                _showMessageWithNoRewards = value;
                NotifyOfPropertyChange(() => ShowMessageWithNoRewards);
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

        public BindableCollection<RewardDisplayModel> Rewards { get; private set; } = new BindableCollection<RewardDisplayModel>();

        private Notifier _notifier;

        private readonly IGetAllRewardsUseCase _getAllRewardsUseCase;
        private readonly IGetRewardByIdUseCase _getRewardByIdUseCase;
        private readonly IUpdateRewardUseCase _updateRewardUseCase;
        private readonly IGetProfileUseCase _getProfileUseCase;
        private readonly IUpdateProfileUseCase _updateProfileUseCase;

        public RewardViewModel(
            IGetAllRewardsUseCase getAllRewardsUseCase,
            IGetRewardByIdUseCase getRewardByIdUseCase,
            IUpdateRewardUseCase updateRewardUseCase,
            IGetProfileUseCase getProfileUseCase,
            IUpdateProfileUseCase updateProfileUseCase,
            Notifier notifier)
        {
            _getAllRewardsUseCase = getAllRewardsUseCase;
            _getRewardByIdUseCase = getRewardByIdUseCase;
            _updateRewardUseCase = updateRewardUseCase;
            _getProfileUseCase = getProfileUseCase;
            _updateProfileUseCase = updateProfileUseCase;
            _notifier = notifier;
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            Loading = true;

            _ = LoadRewardsAsync();
        }

        private async System.Threading.Tasks.Task LoadRewardsAsync()
        {
            var rewards = await _getAllRewardsUseCase.Execute();
            var profile = await _getProfileUseCase.Execute();

            Rewards.Clear();
            Rewards.AddRange(rewards);

            ShowMessageWithNoRewards = Rewards.Count == 0;
            Loading = false;
            TotalGold = profile.GoldBalance + "g";
        }

        public void ClaimReward(int id)
        {
            _ = ClaimRewardAsync(id);
        }

        public async System.Threading.Tasks.Task ClaimRewardAsync(int id)
        {
            var reward = await _getRewardByIdUseCase.Execute(id);

            int goldTotal = int.Parse(TotalGold.Replace("g", ""));
            int rewardGold = int.Parse(reward.Gold.Replace("g", ""));

            if (goldTotal < rewardGold)
            {
                ShowMessageFlashAsync("Error", ["Insufficient Gold! You need more gold to claim this reward."]);
                return;
            }

            var success = await _updateRewardUseCase.Execute(id, new RewardDisplayModel
            {
                Id = reward.Id,
                Name = reward.Name,
                Gold = reward.Gold,
                Active = false
            });

            if (success)
            {
                await UpdateProfileAsync(reward.Gold);

                ShowMessageFlashAsync("Success", ["Reward claimed successfully!"]);
                await LoadRewardsAsync();
            }
            else
            {
                ShowMessageFlashAsync("Error", ["Failed to claim the reward."]);
            }
        }

        public async System.Threading.Tasks.Task UpdateProfileAsync(string gold)
        {
            var profile = await _getProfileUseCase.Execute();

            profile.ClaimedRewards += 1;

            int goldToAdd = int.Parse(gold.Replace("g", ""));

            profile.GoldSpent += goldToAdd;
            profile.GoldBalance -= goldToAdd;

            await _updateProfileUseCase.Execute(profile);
        }

        public void EditReward(int id) => _ = ActiveView.OpenItem<EditRewardViewModel>(id);

        public void AddReward() => _ = ActiveView.OpenItem<CreateRewardViewModel>();

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