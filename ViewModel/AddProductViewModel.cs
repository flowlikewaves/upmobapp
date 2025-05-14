using System.Collections.ObjectModel;
using System.Windows.Input;
using Mobappg4v2.Model;

namespace Mobappg4v2.ViewModel
{
    [QueryProperty(nameof(Product), "Product")]
    public class AddProductViewModel : BaseViewModel
    {
        private string _name;
        private string _description;
        private decimal _price;
        private int _stockQuantity;
        private string _category;
        private string _brand;
        private string _sku;
        private double _weight;
        private string _weightUnit;
        private decimal _discountPercentage;
        private ObservableCollection<string> _images;
        private bool _isEditMode;
        private ProductModel _product;

        public ProductModel Product
        {
            get => _product;
            set
            {
                _product = value;
                if (_product != null)
                {
                    LoadProduct();
                }
            }
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public decimal Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        public int StockQuantity
        {
            get => _stockQuantity;
            set => SetProperty(ref _stockQuantity, value);
        }

        public string Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        public string Brand
        {
            get => _brand;
            set => SetProperty(ref _brand, value);
        }

        public string SKU
        {
            get => _sku;
            set => SetProperty(ref _sku, value);
        }

        public double Weight
        {
            get => _weight;
            set => SetProperty(ref _weight, value);
        }

        public string WeightUnit
        {
            get => _weightUnit;
            set => SetProperty(ref _weightUnit, value);
        }

        public decimal DiscountPercentage
        {
            get => _discountPercentage;
            set => SetProperty(ref _discountPercentage, value);
        }

        public ObservableCollection<string> Images
        {
            get => _images;
            set => SetProperty(ref _images, value);
        }

        public ICommand AddImagesCommand { get; }
        public ICommand SaveProductCommand { get; }
        public ICommand CancelCommand { get; }

        public AddProductViewModel()
        {
            Title = "Add Product";
            Images = new ObservableCollection<string>();
            WeightUnit = "kg"; // Default weight unit

            AddImagesCommand = new Command(async () => await AddImages());
            SaveProductCommand = new Command(async () => await SaveProduct());
            CancelCommand = new Command(async () => await Cancel());
        }

        private void LoadProduct()
        {
            _isEditMode = true;
            Title = "Edit Product";
            Name = Product.Name;
            Description = Product.Description;
            Price = Product.Price;
            StockQuantity = Product.StockQuantity;
            Category = Product.Category;
            Brand = Product.Brand;
            SKU = Product.SKU;
            Weight = Product.Weight;
            WeightUnit = Product.WeightUnit;
            DiscountPercentage = Product.DiscountPercentage;
            Images = new ObservableCollection<string>(Product.Images ?? new List<string>());
        }

        private async Task AddImages()
        {
            try
            {
                var result = await FilePicker.PickMultipleAsync(new PickOptions
                {
                    FileTypes = FilePickerFileType.Images
                });

                if (result != null)
                {
                    foreach (var image in result)
                    {
                        Images.Add(image.FullPath);
                    }
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Failed to add images: " + ex.Message, "OK");
            }
        }

        private async Task SaveProduct()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                await Shell.Current.DisplayAlert("Error", "Product name is required", "OK");
                return;
            }

            if (Price <= 0)
            {
                await Shell.Current.DisplayAlert("Error", "Price must be greater than 0", "OK");
                return;
            }

            var product = _isEditMode ? Product : new ProductModel();
            product.Name = Name;
            product.Description = Description;
            product.Price = Price;
            product.StockQuantity = StockQuantity;
            product.Category = Category;
            product.Brand = Brand;
            product.SKU = SKU;
            product.Weight = Weight;
            product.WeightUnit = WeightUnit;
            product.DiscountPercentage = DiscountPercentage;
            product.Images = Images.ToList();

            // TODO: Save product to database or service
            var message = _isEditMode ? "Product updated successfully" : "Product saved successfully";
            await Shell.Current.DisplayAlert("Success", message, "OK");
            await Shell.Current.GoToAsync("//seller");
        }

        private async Task Cancel()
        {
            await Shell.Current.GoToAsync("//seller");
        }
    }
} 