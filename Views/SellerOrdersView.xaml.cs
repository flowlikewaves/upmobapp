using Mobappg4v2.ViewModel;

namespace Mobappg4v2.Views;

public partial class SellerOrdersView : ContentPage
{
    public SellerOrdersView()
    {
        InitializeComponent();
        BindingContext = new SellerOrdersViewModel();
    }
} 