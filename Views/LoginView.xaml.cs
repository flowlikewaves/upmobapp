using Mobappg4v2.ViewModel;

namespace Mobappg4v2.Views;

public partial class LoginView : ContentPage
{
	public LoginView()
	{
		InitializeComponent();
        BindingContext = new LoginViewModel();
    }
}