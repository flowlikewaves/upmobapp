using System.Collections.ObjectModel;
using System.Windows.Input;
using Mobappg4v2.Model;
using Mobappg4v2.Services;
using Mobappg4v2.Helpers;

namespace Mobappg4v2.ViewModel
{
    public class SellerRegistrationViewModel : BaseViewModel
    {
        private readonly MockAuthService _authService;

        private string _email;
        private string _password;
        private string _confirmPassword;
        private string _storeName;
        private string _storeDescription;
        private string _selectedBusinessType;
        private string _phoneNumber;
        private string _address;
        private bool _acceptTerms;

        public string Email
        {
            get => _email;
            set
            {
                SetProperty(ref _email, value);
                OnPropertyChanged(nameof(CanRegister));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
                OnPropertyChanged(nameof(CanRegister));
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                SetProperty(ref _confirmPassword, value);
                OnPropertyChanged(nameof(CanRegister));
            }
        }

        public string StoreName
        {
            get => _storeName;
            set
            {
                SetProperty(ref _storeName, value);
                OnPropertyChanged(nameof(CanRegister));
            }
        }

        public string StoreDescription
        {
            get => _storeDescription;
            set => SetProperty(ref _storeDescription, value);
        }

        public ObservableCollection<string> BusinessTypes { get; } = new ObservableCollection<string>
        {
            "Retail",
            "Wholesale",
            "Manufacturing",
            "Services",
            "Food & Beverage",
            "Other"
        };

        public string SelectedBusinessType
        {
            get => _selectedBusinessType;
            set => SetProperty(ref _selectedBusinessType, value);
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                SetProperty(ref _phoneNumber, value);
                OnPropertyChanged(nameof(CanRegister));
            }
        }

        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        public bool AcceptTerms
        {
            get => _acceptTerms;
            set
            {
                SetProperty(ref _acceptTerms, value);
                OnPropertyChanged(nameof(CanRegister));
            }
        }

        public bool CanRegister =>
            !string.IsNullOrWhiteSpace(Email) &&
            !string.IsNullOrWhiteSpace(Password) &&
            !string.IsNullOrWhiteSpace(ConfirmPassword) &&
            Password == ConfirmPassword &&
            !string.IsNullOrWhiteSpace(StoreName) &&
            !string.IsNullOrWhiteSpace(PhoneNumber) &&
            AcceptTerms;

        public ICommand RegisterCommand { get; }
        public ICommand CancelCommand { get; }

        public SellerRegistrationViewModel()
        {
            _authService = new MockAuthService();
            RegisterCommand = new Command(async () => await Register());
            CancelCommand = new Command(async () => await Cancel());
            SelectedBusinessType = BusinessTypes[0];
        }

        private async Task Register()
        {
            if (!CanRegister)
                return;

            try
            {
                IsBusy = true;

                // Register the seller
                var user = await _authService.RegisterSeller(Email, Password);

                // Store user info in session
                UserSession.Email = user.Email;
                UserSession.UserId = user.UserId;
                UserSession.Role = user.Role;

                // Create store settings
                var storeSettings = new StoreSettingsModel
                {
                    StoreId = user.UserId,
                    StoreName = StoreName,
                    Description = StoreDescription,
                    BusinessType = SelectedBusinessType,
                    Email = Email,
                    Phone = PhoneNumber,
                    Address = Address
                };

                // In a real app, save store settings to database
                await Task.Delay(500); // Simulate saving

                await Application.Current.MainPage.DisplayAlert(
                    "Success", 
                    "Your seller account has been created successfully!", 
                    "OK");

                // Navigate to seller dashboard
                await Shell.Current.GoToAsync("//seller/dashboard");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Registration Failed",
                    ex.Message,
                    "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task Cancel()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
} 