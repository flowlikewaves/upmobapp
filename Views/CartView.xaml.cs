using Mobappg4v2.ViewModel;

namespace Mobappg4v2.Views;

public partial class CartView : ContentPage
{
    public CartView()
    {
        InitializeComponent();
        BindingContext = new CartViewModel();
    }    
}