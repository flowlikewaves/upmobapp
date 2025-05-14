using Mobappg4v2.ViewModel;

namespace Mobappg4v2.Views;

public partial class SellerDashboardView : ContentPage
{
    public SellerDashboardView()
    {
        InitializeComponent();
        BindingContext = new SellerDashboardViewModel();
    }
} 