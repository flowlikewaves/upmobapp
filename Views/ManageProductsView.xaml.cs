using Mobappg4v2.ViewModel;

namespace Mobappg4v2.Views;

public partial class ManageProductsView : ContentPage
{
    public ManageProductsView()
    {
        InitializeComponent();
        BindingContext = new ManageProductsViewModel();
    }
} 