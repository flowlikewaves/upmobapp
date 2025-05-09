using Mobappg4v2.ViewModel;

namespace Mobappg4v2.Views;

public partial class HomePageView : ContentPage
{
    public HomePageView()
    {
        InitializeComponent();
        BindingContext = new HomePageViewModel();
    }

}