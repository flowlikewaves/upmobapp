using System.Collections.ObjectModel;
using System.Windows.Input;
using Mobappg4v2.Model;

namespace Mobappg4v2.ViewModel
{
    public class ManageProductsViewModel : BaseViewModel
    {
        private ObservableCollection<ProductModel> _products;
        private string _searchQuery;
        private bool _isRefreshing;

        public ObservableCollection<ProductModel> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                if (SetProperty(ref _searchQuery, value))
                {
                    SearchProducts();
                }
            }
        }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        public ICommand SearchCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand AddNewProductCommand { get; }
        public ICommand EditProductCommand { get; }
        public ICommand DeleteProductCommand { get; }

        private ObservableCollection<ProductModel> _allProducts; // Keeps all products for filtering

        public ManageProductsViewModel()
        {
            Title = "Manage Products";
            Products = new ObservableCollection<ProductModel>();
            _allProducts = new ObservableCollection<ProductModel>();

            SearchCommand = new Command(() => SearchProducts());
            RefreshCommand = new Command(async () => await RefreshProducts());
            AddNewProductCommand = new Command(async () => await AddNewProduct());
            EditProductCommand = new Command<ProductModel>(async (product) => await EditProduct(product));
            DeleteProductCommand = new Command<ProductModel>(async (product) => await DeleteProduct(product));

            LoadDummyData(); // For testing purposes
        }

        private void SearchProducts()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                Products = new ObservableCollection<ProductModel>(_allProducts);
                return;
            }

            var filteredProducts = _allProducts.Where(p =>
                p.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                p.SKU.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                p.Description.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));

            Products = new ObservableCollection<ProductModel>(filteredProducts);
        }

        private async Task RefreshProducts()
        {
            IsRefreshing = true;

            try
            {
                // TODO: Load products from database or service
                await Task.Delay(1000); // Simulating network delay
                LoadDummyData();
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        private async Task AddNewProduct()
        {
            await Shell.Current.GoToAsync("//seller/add-product");
        }

        private async Task EditProduct(ProductModel product)
        {
            var parameters = new Dictionary<string, object>
            {
                { "Product", product }
            };
            await Shell.Current.GoToAsync("//seller/add-product", parameters);
        }

        private async Task DeleteProduct(ProductModel product)
        {
            bool answer = await Shell.Current.DisplayAlert(
                "Confirm Delete",
                $"Are you sure you want to delete {product.Name}?",
                "Yes", "No");

            if (answer)
            {
                // TODO: Delete product from database or service
                _allProducts.Remove(product);
                Products.Remove(product);
                await Shell.Current.DisplayAlert("Success", "Product deleted successfully", "OK");
            }
        }

        private void LoadDummyData()
        {
            var dummyProducts = new List<ProductModel>
            {
                new ProductModel
                {
                    ProductId = "1",
                    Name = "Premium T-Shirt",
                    Description = "High-quality cotton t-shirt",
                    Price = 29.99m,
                    StockQuantity = 100,
                    Category = "Clothing",
                    Brand = "FashionBrand",
                    SKU = "TSH001",
                    Images = new List<string> { "tshirt_placeholder.png" }
                },
                new ProductModel
                {
                    ProductId = "2",
                    Name = "Designer Jeans",
                    Description = "Slim-fit designer jeans",
                    Price = 89.99m,
                    StockQuantity = 50,
                    Category = "Clothing",
                    Brand = "DenimCo",
                    SKU = "JNS002",
                    Images = new List<string> { "jeans_placeholder.png" }
                },
                new ProductModel
                {
                    ProductId = "3",
                    Name = "Running Shoes",
                    Description = "Comfortable running shoes",
                    Price = 119.99m,
                    StockQuantity = 75,
                    Category = "Footwear",
                    Brand = "SportyFeet",
                    SKU = "SHO003",
                    Images = new List<string> { "shoes_placeholder.png" }
                }
            };

            _allProducts = new ObservableCollection<ProductModel>(dummyProducts);
            Products = new ObservableCollection<ProductModel>(dummyProducts);
        }
    }
} 