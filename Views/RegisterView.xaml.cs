using Mobappg4v2.ViewModel;

namespace Mobappg4v2.Views;

public partial class RegisterView : ContentPage
{
	public RegisterView()
	{
		InitializeComponent();
        BindingContext = new RegisterViewModel();
    }
}