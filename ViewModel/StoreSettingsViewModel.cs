using System.Collections.ObjectModel;
using System.Windows.Input;
using Mobappg4v2.Model;

namespace Mobappg4v2.ViewModel
{
    public class StoreSettingsViewModel : BaseViewModel
    {
        private StoreSettingsModel _settings;
        private ObservableCollection<PaymentMethodModel> _paymentMethods;

        public StoreSettingsModel Settings
        {
            get => _settings;
            set => SetProperty(ref _settings, value);
        }

        public ObservableCollection<PaymentMethodModel> PaymentMethods
        {
            get => _paymentMethods;
            set => SetProperty(ref _paymentMethods, value);
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

        public ICommand ChangeBannerCommand { get; }
        public ICommand ChangeLogoCommand { get; }
        public ICommand ManageShippingZonesCommand { get; }
        public ICommand SaveChangesCommand { get; }

        public StoreSettingsViewModel()
        {
            Title = "Store Settings";
            LoadDummyData(); // For testing purposes

            ChangeBannerCommand = new Command(async () => await ChangeBanner());
            ChangeLogoCommand = new Command(async () => await ChangeLogo());
            ManageShippingZonesCommand = new Command(async () => await ManageShippingZones());
            SaveChangesCommand = new Command(async () => await SaveChanges());
        }

        private void LoadDummyData()
        {
            Settings = new StoreSettingsModel
            {
                StoreName = "My Awesome Store",
                Description = "Your one-stop shop for everything awesome",
                LogoUrl = "store_logo_placeholder.png",
                BannerUrl = "store_banner_placeholder.png",
                BusinessType = "Retail",
                Email = "store@example.com",
                Phone = "+1234567890",
                Address = "123 Store Street",
                City = "Store City",
                State = "Store State",
                Country = "Store Country",
                PostalCode = "12345",
                OffersLocalDelivery = true,
                OffersInternationalShipping = false,
                MinimumOrderAmount = 10.00m,
                FreeShippingThreshold = 50.00m
            };

            PaymentMethods = new ObservableCollection<PaymentMethodModel>
            {
                new PaymentMethodModel { Name = "Credit Card", IsSelected = true },
                new PaymentMethodModel { Name = "PayPal", IsSelected = true },
                new PaymentMethodModel { Name = "Bank Transfer", IsSelected = false },
                new PaymentMethodModel { Name = "Cash on Delivery", IsSelected = true }
            };
        }

        private async Task ChangeBanner()
        {
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    FileTypes = FilePickerFileType.Images
                });

                if (result != null)
                {
                    Settings.BannerUrl = result.FullPath;
                    OnPropertyChanged(nameof(Settings));
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Failed to change banner: " + ex.Message, "OK");
            }
        }

        private async Task ChangeLogo()
        {
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    FileTypes = FilePickerFileType.Images
                });

                if (result != null)
                {
                    Settings.LogoUrl = result.FullPath;
                    OnPropertyChanged(nameof(Settings));
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Failed to change logo: " + ex.Message, "OK");
            }
        }

        private async Task ManageShippingZones()
        {
            // TODO: Navigate to shipping zones management page
            await Shell.Current.DisplayAlert("Coming Soon", "Shipping zones management will be available soon.", "OK");
        }

        private async Task SaveChanges()
        {
            try
            {
                IsBusy = true;

                // Update accepted payment methods
                Settings.AcceptedPaymentMethods = PaymentMethods
                    .Where(pm => pm.IsSelected)
                    .Select(pm => pm.Name)
                    .ToList();

                // TODO: Save settings to database or service
                await Task.Delay(1000); // Simulate saving

                await Shell.Current.DisplayAlert("Success", "Store settings saved successfully!", "OK");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Failed to save settings: " + ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }

    public class PaymentMethodModel
    {
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
} 