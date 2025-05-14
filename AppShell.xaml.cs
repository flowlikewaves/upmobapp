using Microsoft.Maui.Controls;
using Mobappg4v2.Views;

namespace Mobappg4v2
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            // Register seller routes
            Routing.RegisterRoute("seller/dashboard", typeof(SellerDashboardView));
            Routing.RegisterRoute("seller/add-product", typeof(AddProductView));
            Routing.RegisterRoute("seller/orders", typeof(SellerOrdersView));
            Routing.RegisterRoute("seller/manage-products", typeof(ManageProductsView));
            Routing.RegisterRoute("seller/settings", typeof(StoreSettingsView));
            Routing.RegisterRoute("seller/order-details", typeof(OrderDetailsView));

            // Register customer routes
            Routing.RegisterRoute("customer/home", typeof(HomePageView));
            Routing.RegisterRoute("customer/cart", typeof(CartView));
            Routing.RegisterRoute("customer/profile", typeof(ProfileView));
            Routing.RegisterRoute("customer/orders", typeof(OrderDetailsView));
        }
    }
} 