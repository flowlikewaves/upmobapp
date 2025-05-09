using Mobappg4v2.Helpers;
using Mobappg4v2.Services;
using Mobappg4v2.Views;
using System.Windows.Input;

namespace Mobappg4v2.ViewModel
{
    public class LoginViewModel : BaseViewModel
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

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand ForgotPasswordCommand { get; }

        public LoginViewModel()
        {
            _authService = new FirebaseAuthService();

            LoginCommand = new Command(async () => await Login());
            RegisterCommand = new Command(SignUp);
            ForgotPasswordCommand = new Command(ForgotPassword);
        }

        private void ForgotPassword()
        {
            // TODO: Navigate to ForgotPasswordView or show popup
        }

        private async void SignUp()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new RegisterView());
        }

        private async Task Login()
        {
            try
            {
                var auth = await _authService.SignIn(Email, Password);
                string token = await _authService.GetFreshToken(auth);

                // Store email in session
                UserSession.Email = auth.User.Email;

                await ToastHelper.ShowToast($"Welcome {auth.User.Email}");

                Application.Current.MainPage = new AppShell();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Login Failed", ex.Message, "OK");
            }
        }
    }
}
