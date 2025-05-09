using Mobappg4v2.Helpers;
using Mobappg4v2.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mobappg4v2.ViewModel
{
    public class FinishCartViewModel : BaseViewModel
    {
        private DeliveryTypeModel _DeliveryType;
        public DeliveryTypeModel DeliveryType
        {
            get => _DeliveryType;
            set => SetProperty(ref _DeliveryType, value);
        }

        private AddressModel _PrimaryAddress;
        public AddressModel PrimaryAddress
        {
            get => _PrimaryAddress;
            set
            {
                if (_PrimaryAddress != value)
                {
                    _PrimaryAddress = value;
                    OnPropertyChanged(nameof(PrimaryAddress));
                    OnPropertyChanged(nameof(FullAddress));
                }
            }
        }

        private ObservableCollection<ProductListModel> _Products = [];
        public ObservableCollection<ProductListModel> Products
        {
            get => _Products;
            set => SetProperty(ref _Products, value);
        }

        public string FullAddress
        {
            get
            {
                return $"{PrimaryAddress.StreetOne}, {PrimaryAddress.StreetTwo}, {PrimaryAddress.City}, {PrimaryAddress.State}";
            }
        }

        private bool _IsLoaded = false;
        public bool IsLoaded
        {
            get => _IsLoaded;
            set => SetProperty(ref _IsLoaded, value);
        }
        public ICommand FinishCommand { get; }
        public ICommand BackCommand { get; }

        public FinishCartViewModel(ObservableCollection<ProductListModel> products, DeliveryTypeModel deliveryType, AddressModel address)
        {
            DeliveryType = deliveryType;
            Products = products;
            PrimaryAddress = address;        
            FinishCommand = new Command(FinishOrder);
            BackCommand = new Command(GoBack);
            IsLoaded = true;

        }
        private async void FinishOrder()
        {            
            await Application.Current.MainPage.Navigation.PopToRootAsync();
            await Shell.Current.GoToAsync("///HomePageView");
            await ToastHelper.ShowToast("Order Complete");
        }
        private async void GoBack(object obj)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
