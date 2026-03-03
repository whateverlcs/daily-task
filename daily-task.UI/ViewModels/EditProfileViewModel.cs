using Caliburn.Micro;
using daily_task.Application.Models;
using daily_task.Application.UseCases.Profile.GetProfile;
using daily_task.Application.UseCases.Profile.Update;
using daily_task.Exceptions.ExceptionsBase;
using ToastNotifications;
using ToastNotifications.Messages;

namespace daily_task.UI.ViewModels
{
    public class EditProfileViewModel : Screen
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

        private string _username = string.Empty;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;

                if (_profileData.Id is not 0)
                    _profileData.User = value;

                NotifyOfPropertyChange(() => Username);
            }
        }

        private ProfileDisplayModel _profileData = new();

        private Notifier _notifier;
        private readonly IUpdateProfileUseCase _updateProfileUseCase;
        private readonly IGetProfileUseCase _getProfileUseCase;

        public EditProfileViewModel(
            IUpdateProfileUseCase updateProfileUseCase,
            IGetProfileUseCase getProfileUseCase,
            Notifier notifier)
        {
            _updateProfileUseCase = updateProfileUseCase;
            _getProfileUseCase = getProfileUseCase;
            _notifier = notifier;
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            _ = LoadFields();
        }

        public async System.Threading.Tasks.Task LoadFields()
        {
            var profile = await _getProfileUseCase.Execute();

            if (profile != null)
            {
                Username = profile.User;

                _profileData = profile;
            }
        }

        public void EditUsername()
        {
            EnableFields = false;
            Loading = true;
            _ = EditUsernameAsync();
        }

        private async System.Threading.Tasks.Task EditUsernameAsync()
        {
            try
            {
                var sucess = await _updateProfileUseCase.Execute(_profileData);

                if (sucess)
                {
                    ShowMessageFlashAsync("Success", ["Username updated successfully!"]);
                }
                else
                {
                    ShowMessageFlashAsync("Error", ["An error occurred while updating the username."]);
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

        public void ReturnToProfile() => _ = ActiveView.OpenItem<ProfileViewModel>();
    }
}