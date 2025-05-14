using System.Collections.ObjectModel;
using System.Windows.Input;
using Mobappg4v2.Model;
using Mobappg4v2.Views;
using Mobappg4v2.Helpers;

namespace Mobappg4v2.ViewModel
{
    public class SellerDashboardViewModel : BaseViewModel
    {
        private string _storeName;
        private string _sellerName;
        private string _profileImage;
        private double _rating;
        private int _totalSales;
        private int _todaySales;
        private int _pendingOrders;
        private int _totalProducts;
        private ObservableCollection<OrderDetailsModel> _recentOrders;

        public string StoreName
        {
            get => _storeName;
            set => SetProperty(ref _storeName, value);
        }

        public string SellerName
        {
            get => _sellerName;
            set => SetProperty(ref _sellerName, value);
        }

        public string ProfileImage
        {
            get => _profileImage;
            set => SetProperty(ref _profileImage, value);
        }

        public double Rating
        {
            get => _rating;
            set => SetProperty(ref _rating, value);
        }

        public int TotalSales
        {
            get => _totalSales;
            set => SetProperty(ref _totalSales, value);
        }

        public int TodaySales
        {
            get => _todaySales;
            set => SetProperty(ref _todaySales, value);
        }

        public int PendingOrders
        {
            get => _pendingOrders;
            set => SetProperty(ref _pendingOrders, value);
        }

        public int TotalProducts
        {
            get => _totalProducts;
            set => SetProperty(ref _totalProducts, value);
        }

        public ObservableCollection<OrderDetailsModel> RecentOrders
        {
            get => _recentOrders;
            set => SetProperty(ref _recentOrders, value);
        }

        public ICommand AddProductCommand { get; }
        public ICommand ViewOrdersCommand { get; }
        public ICommand ManageProductsCommand { get; }
        public ICommand StoreSettingsCommand { get; }
        public ICommand LogoutCommand { get; }

        public SellerDashboardViewModel()
        {
            Title = "Seller Dashboard";
            LoadDummyData(); // For testing purposes

            AddProductCommand = new Command(async () => await AddProduct());
            ViewOrdersCommand = new Command(async () => await ViewOrders());
            ManageProductsCommand = new Command(async () => await ManageProducts());
            StoreSettingsCommand = new Command(async () => await StoreSettings());
            LogoutCommand = new Command(async () => await Logout());
        }

        private void LoadDummyData()
        {
            StoreName = "My Awesome Store";
            SellerName = "John Doe";
            ProfileImage = "profile_placeholder.png";
            Rating = 4.5;
            TotalSales = 150;
            TodaySales = 5;
            PendingOrders = 3;
            TotalProducts = 45;

            RecentOrders = new ObservableCollection<OrderDetailsModel>
            {
                new OrderDetailsModel { OrderId = "ORD001", OrderDate = DateTime.Now, Status = "Pending", Total = 99.99m },
                new OrderDetailsModel { OrderId = "ORD002", OrderDate = DateTime.Now.AddHours(-2), Status = "Processing", Total = 149.99m },
                new OrderDetailsModel { OrderId = "ORD003", OrderDate = DateTime.Now.AddHours(-5), Status = "Shipped", Total = 79.99m }
            };
        }

        private async Task AddProduct()
        {
            await Shell.Current.GoToAsync("//seller/add-product");
        }

        private async Task ViewOrders()
        {
            await Shell.Current.GoToAsync("//seller/orders");
        }

        private async Task ManageProducts()
        {
            await Shell.Current.GoToAsync("//seller/manage-products");
        }

        private async Task StoreSettings()
        {
            await Shell.Current.GoToAsync("//seller/settings");
        }

        private async Task Logout()
        {
            bool answer = await Shell.Current.DisplayAlert(
                "Logout",
                "Are you sure you want to logout?",
                "Yes", "No");

            if (answer)
            {
                // Clear user session
                UserSession.Email = null;
                UserSession.UserId = null;
                UserSession.Role = UserRole.Customer; // Reset to default

                // Navigate to login page
                await Shell.Current.GoToAsync("//login");
            }
        }
    }
} 