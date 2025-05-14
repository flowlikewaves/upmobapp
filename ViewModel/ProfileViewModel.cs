using Mobappg4v2.Model;
using Mobappg4v2.Views;
using System.Windows.Input;
using Mobappg4v2.Helpers;

namespace Mobappg4v2.ViewModel
{
    public class ProfileViewModel : BaseViewModel
    {        
        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string ImageUrl { get; set; } = "https://cdn-icons-png.flaticon.com/512/149/149071.png";

        private List<MenuItems> _MenuItems = [];
        public List<MenuItems> MenuItems
        {
            get => _MenuItems;
            set => SetProperty(ref _MenuItems, value);
        }

        private bool _IsLoaded;
        public bool IsLoaded
        {
            get => _IsLoaded;
            set => SetProperty(ref _IsLoaded, value);
        }

        public ICommand SelectMenuCommand { get; }
        public ProfileViewModel()
        {
            SelectMenuCommand = new Command<MenuItems>(async (item) => await SelectMenu(item));
            Email = UserSession.Email ?? "user@email.com";
            _ = InitializeAsync();
        }
        private async Task InitializeAsync()
        {
            await PopulateDataAsync();
        }
        async Task PopulateDataAsync()
        {
            await Task.Delay(500);
            //TODO: Remove Delay here and call API if needed
            MenuItems.Clear();
            //MenuItems.Add(new MenuItems() { Title = "Edit Profile", Body = "\uf3eb" });
            MenuItems.Add(new MenuItems() { Title = "Shipping Address", Body = "\uf34e", TargetType = typeof(ShippingAddressView) });
            MenuItems.Add(new MenuItems() { Title = "Wishlist", Body = "\uf2d5", TargetType = typeof(WishListView) });
            MenuItems.Add(new MenuItems() { Title = "Order History", Body = "\uf150", TargetType = typeof(OrderDetailsView) });
            MenuItems.Add(new MenuItems() { Title = "Track Order", Body = "\uf787", TargetType = typeof(OrderDetailsView) });
            //MenuItems.Add(new MenuItems() { Title = "Notifications", Body = "\uf09c"});
            MenuItems.Add(new MenuItems() { Title = "Logout", Body = "\uf343", TargetType = typeof(LoginView) });
            IsLoaded = true;
        }

        private async Task SelectMenu(MenuItems item)
        {
            if (item.TargetType != null)
            {
                if (item.TargetType == typeof(LoginView))
                {
                    var response = await App.Current.MainPage.DisplayAlert("Logout", "Do you want to logout?", "Yes", "No");
                    if (response)
                        App.Current.MainPage = new LoginView();
                }
                else
                {
                    await Application.Current.MainPage.Navigation.PushAsync(((Page)Activator.CreateInstance(item.TargetType)));
                }
            }
        }       
    }
}
