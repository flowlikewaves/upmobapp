using Mobappg4v2.Services;
using Mobappg4v2.Views;
using System.Windows.Input;
using Mobappg4v2.Helpers;

namespace Mobappg4v2.ViewModel
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly FirebaseAuthService _authService;

        private string _Email;
        public string Email
        {
            get => _Email;
            set => SetProperty(ref _Email, value);
        }

        private string _Password;
        public string Password
        {
            get => _Password;
            set => SetProperty(ref _Password, value);
        }

        private string _RepeatPassword;
        public string RepeatPassword
        {
            get => _RepeatPassword;
            set => SetProperty(ref _RepeatPassword, value);
        }

        private string _Name;
        public string Name
        {
            get => _Name;
            set => SetProperty(ref _Name, value);
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public RegisterViewModel()
        {
            _authService = new FirebaseAuthService();

            LoginCommand = new Command(Login);
            RegisterCommand = new Command(async () => await SignUp());
        }

        private async Task SignUp()
        {
            if (Password != RepeatPassword)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }

            try
            {
                var auth = await _authService.SignUp(Email, Password);
                var token = await _authService.GetFreshToken(auth);

                // Store email in session
                UserSession.Email = auth.User.Email;

                await Application.Current.MainPage.DisplayAlert("Success", $"Account created for {auth.User.Email}", "OK");

                Application.Current.MainPage = new LoginView();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Registration Failed", ex.Message, "OK");
            }
        }

        private void Login()
        {
            Application.Current.MainPage = new LoginView();
        }
    }
}
