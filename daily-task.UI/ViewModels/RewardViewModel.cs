using Caliburn.Micro;
using daily_task.Application.Models;
using daily_task.Application.UseCases.Profile.GetProfile;
using daily_task.Application.UseCases.Reward.GetAllRewards;
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
        private readonly IGetProfileUseCase _getProfileUseCase;

        public RewardViewModel(
            IGetAllRewardsUseCase getAllRewardsUseCase,
            IGetProfileUseCase getProfileUseCase,
            Notifier notifier)
        {
            _getAllRewardsUseCase = getAllRewardsUseCase;
            _getProfileUseCase = getProfileUseCase;
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