using Mobappg4v2.Model;
using System.Windows.Input;

namespace Mobappg4v2.ViewModel
{
    public class ProductDetailsViewModel : BaseViewModel
    {
        double lastScrollIndex;
        double currentScrollIndex;       

        private bool _IsFooterVisible = false;
        public bool IsFooterVisible
        {
            get => _IsFooterVisible;
            set => SetProperty(ref _IsFooterVisible, value);
        }

        private bool _IsFavorite = false;
        public bool IsFavorite
        {
            get => _IsFavorite;
            set
            {
                if ( _IsFavorite != value)
                {
                    _IsFavorite = value;
                    OnPropertyChanged(nameof(IsFavorite));
                    OnPropertyChanged(nameof(FavStatusColor));
                }
            }
        }
        public Color FavStatusColor
        {
            get
            {
                if (IsFavorite)
                {
                    return Color.FromArgb("#00C569");
                }
                return Color.FromArgb("#000000");
            }
        }

        private ProductDetail _ProductDetail = new();
        public ProductDetail ProductDetail
        {
            get => _ProductDetail;
            set => SetProperty(ref _ProductDetail, value);
        }

        private bool _IsLoaded;
        public bool IsLoaded
        {
            get => _IsLoaded;
            set => SetProperty(ref _IsLoaded, value);
        }
        public ICommand BackCommand { get; }
        public ICommand FavCommand { get; }
        public ICommand AddToCartCommand { get; }

        public ProductDetailsViewModel()
        {
            BackCommand = new Command<object>(GoBack);
            FavCommand = new Command<Color>(FavItem);
            AddToCartCommand = new Command(AddToCart);
            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            await PopulateDataAsync();
        }

        async Task PopulateDataAsync()
        {
            await Task.Delay(500);
            ProductDetail.Price = 0;
            ProductDetail.Name = "Sample Product Page";
            ProductDetail.ImageUrl = "default_product.png";
            ProductDetail.Colors = Color.FromArgb("#B1763F");
            ProductDetail.Details = "This is a sample product details page. Here you can showcase a local Filipino product, display its price, available colors, and a description. You can also show customer reviews below. Replace this text with real product information when you go live.";

            List<ReviewModel> reviewData =
            [
                new ReviewModel() { ImageUrl = "default_user.png", Name = "Maria Clara", Review = "This page is a great way to highlight your handmade Filipino products!", Rating = 5 },
                new ReviewModel() { ImageUrl = "default_user.png", Name = "Juan Dela Cruz", Review = "Customers can see product details, price, and reviews here.", Rating = 4 },
            ];
            ProductDetail.Reviews = reviewData;
            IsLoaded = true;
        }
        private async void GoBack(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        private void FavItem(Color obj)
        {
            IsFavorite = true ? !IsFavorite : IsFavorite;
        }
        public void ChageFooterVisibility(double currentY)
        {
            currentScrollIndex = currentY;
            if (currentScrollIndex > lastScrollIndex)
            {
                IsFooterVisible = false;
            }
            else
            {
                IsFooterVisible = true;
            }
            lastScrollIndex = currentScrollIndex;
        }

        private void AddToCart()
        {
           
        }       
    }
}
