using Mobappg4v2.ViewModel;

namespace Mobappg4v2.Views;

public partial class ShippingAddressView : ContentPage
{
	public ShippingAddressView()
	{
		InitializeComponent();
        BindingContext = new ShippingAddressViewModel();

    }
}