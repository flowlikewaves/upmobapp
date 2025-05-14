using Mobappg4v2.ViewModel;

namespace Mobappg4v2.Views;

public partial class AddProductView : ContentPage
{
    public AddProductView()
    {
        InitializeComponent();
        BindingContext = new AddProductViewModel();
    }
} 