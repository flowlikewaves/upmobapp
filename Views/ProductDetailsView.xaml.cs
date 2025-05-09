using Mobappg4v2.ViewModel;

namespace Mobappg4v2.Views;

public partial class ProductDetailsView : ContentPage
{
    public ProductDetailsView()
    {
        InitializeComponent();
        BindingContext = new ProductDetailsViewModel();
    }

}