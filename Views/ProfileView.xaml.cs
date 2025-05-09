using Mobappg4v2.ViewModel;

namespace Mobappg4v2.Views;

public partial class ProfileView : ContentPage
{
    public ProfileView()
    {
        InitializeComponent();
        BindingContext = new ProfileViewModel();
    }
}