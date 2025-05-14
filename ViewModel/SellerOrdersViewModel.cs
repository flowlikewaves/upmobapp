using System.Collections.ObjectModel;
using System.Windows.Input;
using Mobappg4v2.Model;

namespace Mobappg4v2.ViewModel
{
    public class SellerOrdersViewModel : BaseViewModel
    {
        private ObservableCollection<SellerOrderModel> _orders;
        private string _searchQuery;
        private bool _isRefreshing;
        private string _currentFilter = "All";

        public ObservableCollection<SellerOrderModel> Orders
        {
            get => _orders;
            set => SetProperty(ref _orders, value);
        }

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                if (SetProperty(ref _searchQuery, value))
                {
                    SearchOrders();
                }
            }
        }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        // Filter button colors
        public Color AllFilterColor => _currentFilter == "All" ? Color.FromArgb("#007AFF") : Colors.Gray;
        public Color PendingFilterColor => _currentFilter == "Pending" ? Color.FromArgb("#FF9500") : Colors.Gray;
        public Color ProcessingFilterColor => _currentFilter == "Processing" ? Color.FromArgb("#5856D6") : Colors.Gray;
        public Color ShippedFilterColor => _currentFilter == "Shipped" ? Color.FromArgb("#007AFF") : Colors.Gray;
        public Color DeliveredFilterColor => _currentFilter == "Delivered" ? Color.FromArgb("#34C759") : Colors.Gray;
        public Color CancelledFilterColor => _currentFilter == "Cancelled" ? Color.FromArgb("#FF3B30") : Colors.Gray;

        public ICommand SearchCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand FilterCommand { get; }
        public ICommand ViewOrderCommand { get; }
        public ICommand UpdateStatusCommand { get; }

        private ObservableCollection<SellerOrderModel> _allOrders; // Keeps all orders for filtering

        public SellerOrdersViewModel()
        {
            Title = "Orders";
            Orders = new ObservableCollection<SellerOrderModel>();
            _allOrders = new ObservableCollection<SellerOrderModel>();

            SearchCommand = new Command(() => SearchOrders());
            RefreshCommand = new Command(async () => await RefreshOrders());
            FilterCommand = new Command<string>(FilterOrders);
            ViewOrderCommand = new Command<SellerOrderModel>(async (order) => await ViewOrderDetails(order));
            UpdateStatusCommand = new Command<SellerOrderModel>(async (order) => await UpdateOrderStatus(order));

            LoadDummyData(); // For testing purposes
        }

        private void SearchOrders()
        {
            var filteredOrders = _allOrders.Where(o =>
                string.IsNullOrWhiteSpace(SearchQuery) ||
                o.OrderId.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                o.CustomerName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));

            if (_currentFilter != "All")
            {
                filteredOrders = filteredOrders.Where(o => o.Status == _currentFilter);
            }

            Orders = new ObservableCollection<SellerOrderModel>(filteredOrders);
        }

        private void FilterOrders(string status)
        {
            _currentFilter = status;
            OnPropertyChanged(nameof(AllFilterColor));
            OnPropertyChanged(nameof(PendingFilterColor));
            OnPropertyChanged(nameof(ProcessingFilterColor));
            OnPropertyChanged(nameof(ShippedFilterColor));
            OnPropertyChanged(nameof(DeliveredFilterColor));
            OnPropertyChanged(nameof(CancelledFilterColor));

            SearchOrders();
        }

        private async Task RefreshOrders()
        {
            IsRefreshing = true;

            try
            {
                // TODO: Load orders from database or service
                await Task.Delay(1000); // Simulating network delay
                LoadDummyData();
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        private async Task ViewOrderDetails(SellerOrderModel order)
        {
            if (order == null) return;
            
            var parameters = new Dictionary<string, object>
            {
                { "Order", order }
            };
            await Shell.Current.GoToAsync("seller/order-details", parameters);
        }

        private async Task UpdateOrderStatus(SellerOrderModel order)
        {
            string[] statuses = { "Pending", "Processing", "Shipped", "Delivered", "Cancelled" };
            string action = await Shell.Current.DisplayActionSheet(
                "Update Order Status",
                "Cancel",
                null,
                statuses);

            if (action != null && action != "Cancel")
            {
                order.Status = action;
                // TODO: Update order status in database or service
                await Shell.Current.DisplayAlert("Success", "Order status updated successfully", "OK");
                SearchOrders(); // Refresh the list to update colors
            }
        }

        private void LoadDummyData()
        {
            var dummyOrders = new List<SellerOrderModel>
            {
                new SellerOrderModel
                {
                    OrderId = "ORD001",
                    CustomerName = "John Smith",
                    OrderDate = DateTime.Now.AddDays(-1),
                    TotalAmount = 129.99m,
                    Status = "Pending",
                    PaymentStatus = "Paid",
                    PaymentMethod = "Credit Card",
                    Items = new List<OrderItemModel>
                    {
                        new OrderItemModel
                        {
                            ProductName = "Premium T-Shirt",
                            Quantity = 2,
                            UnitPrice = 29.99m,
                            SKU = "TSH001"
                        }
                    }
                },
                new SellerOrderModel
                {
                    OrderId = "ORD002",
                    CustomerName = "Jane Doe",
                    OrderDate = DateTime.Now.AddDays(-2),
                    TotalAmount = 89.99m,
                    Status = "Processing",
                    PaymentStatus = "Paid",
                    PaymentMethod = "PayPal",
                    Items = new List<OrderItemModel>
                    {
                        new OrderItemModel
                        {
                            ProductName = "Designer Jeans",
                            Quantity = 1,
                            UnitPrice = 89.99m,
                            SKU = "JNS002"
                        }
                    }
                },
                new SellerOrderModel
                {
                    OrderId = "ORD003",
                    CustomerName = "Mike Johnson",
                    OrderDate = DateTime.Now.AddDays(-3),
                    TotalAmount = 239.98m,
                    Status = "Shipped",
                    PaymentStatus = "Paid",
                    PaymentMethod = "Credit Card",
                    TrackingNumber = "TRK123456",
                    Items = new List<OrderItemModel>
                    {
                        new OrderItemModel
                        {
                            ProductName = "Running Shoes",
                            Quantity = 2,
                            UnitPrice = 119.99m,
                            SKU = "SHO003"
                        }
                    }
                }
            };

            _allOrders = new ObservableCollection<SellerOrderModel>(dummyOrders);
            Orders = new ObservableCollection<SellerOrderModel>(dummyOrders);
        }
    }
} 