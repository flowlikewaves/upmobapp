using Mobappg4v2.ViewModel;
namespace Mobappg4v2.Views;
public partial class AllProductView : ContentPage
{
    public AllProductView()
    {
        InitializeComponent();
        BindingContext = new AllProductViewModel();
    }
}