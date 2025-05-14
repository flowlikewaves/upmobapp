using Mobappg4v2.ViewModel;

namespace Mobappg4v2.Views;

public partial class SellerRegistrationView : ContentPage
{
    public SellerRegistrationView()
    {
        InitializeComponent();
        BindingContext = new SellerRegistrationViewModel();
    }
} 