using Mobappg4v2.ViewModel;

namespace Mobappg4v2.Views;

public partial class BrandDetailView : ContentPage
{
    public BrandDetailView()
    {
        InitializeComponent();
        BindingContext = new BrandDetailViewModel();
    }
}