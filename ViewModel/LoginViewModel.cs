using Mobappg4v2.Helpers;
using Mobappg4v2.Services;
using Mobappg4v2.Views;
using System.Windows.Input;
using Mobappg4v2.Model;

namespace Mobappg4v2.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly MockAuthService _authService;

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

        private bool _IsSeller;
        public bool IsSeller
        {
            get => _IsSeller;
            set => SetProperty(ref _IsSeller, value);
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand ForgotPasswordCommand { get; }

        public LoginViewModel()
        {
            _authService = new MockAuthService();

            LoginCommand = new Command(async () => await Login());
            RegisterCommand = new Command(async () => await SignUp());
            ForgotPasswordCommand = new Command(ForgotPassword);
        }

        private void ForgotPassword()
        {
            // TODO: Implement forgot password functionality
        }

        private async Task SignUp()
        {
            if (IsSeller)
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new SellerRegistrationView());
            }
            else
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new RegisterView());
            }
        }

        private async Task Login()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Login Failed", 
                    "Please enter both email and password", 
                    "OK");
                return;
            }

            try
            {
                var user = await _authService.SignIn(Email, Password);
                
                // Store user info in session
                UserSession.Email = user.Email;
                UserSession.UserId = user.UserId;
                UserSession.Role = user.Role;

                await ToastHelper.ShowToast($"Welcome {user.Email}");

                // Navigate based on user role
                if (user.Role == UserRole.Seller)
                {
                    await Shell.Current.GoToAsync("//seller/dashboard");
                }
                else
                {
                    await Shell.Current.GoToAsync("//customer/home");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Login Failed",
                    ex.Message,
                    "OK");
            }
        }
    }
}
