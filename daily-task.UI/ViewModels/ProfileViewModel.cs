using Caliburn.Micro;
using daily_task.Application.UseCases.Profile.GetProfile;

namespace daily_task.UI.ViewModels
{
    public class ProfileViewModel : Screen
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

        private string _user = string.Empty;

        public string User
        {
            get { return _user; }
            set
            {
                _user = value;
                NotifyOfPropertyChange(() => User);
            }
        }

        private string _rank = string.Empty;

        public string Rank
        {
            get { return _rank; }
            set
            {
                _rank = value;
                NotifyOfPropertyChange(() => Rank);
            }
        }

        private string _tasksCompleted = string.Empty;

        public string TasksCompleted
        {
            get { return _tasksCompleted; }
            set
            {
                _tasksCompleted = value;
                NotifyOfPropertyChange(() => TasksCompleted);
            }
        }

        private string _goldEarned = string.Empty;

        public string GoldEarned
        {
            get { return _goldEarned; }
            set
            {
                _goldEarned = value;
                NotifyOfPropertyChange(() => GoldEarned);
            }
        }

        private string _claimedRewards = string.Empty;

        public string ClaimedRewards
        {
            get { return _claimedRewards; }
            set
            {
                _claimedRewards = value;
                NotifyOfPropertyChange(() => ClaimedRewards);
            }
        }

        private readonly IGetProfileUseCase _getProfileUseCase;

        public ProfileViewModel(IGetProfileUseCase getProfileUseCase)
        {
            _getProfileUseCase = getProfileUseCase;
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            Loading = true;

            _ = LoadProfileAsync();
        }

        private async System.Threading.Tasks.Task LoadProfileAsync()
        {
            var profile = await _getProfileUseCase.Execute();

            User = profile.User.ToUpper();
            Rank = profile.Rank;
            TasksCompleted = profile.TasksCompleted.ToString();
            GoldEarned = profile.GoldEarned.ToString();
            ClaimedRewards = profile.ClaimedRewards.ToString();
            TotalGold = profile.GoldBalance.ToString();

            Loading = false;
        }

        public void EditUsername() => _ = ActiveView.OpenItem<EditProfileViewModel>();

        public void GoToHome() => _ = ActiveView.OpenItem<IndexViewModel>();

        public void GoToRewards() => _ = ActiveView.OpenItem<RewardViewModel>();
    }
}