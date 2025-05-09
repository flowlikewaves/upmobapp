using Mobappg4v2.Model;
using Mobappg4v2.ViewModel;
using System.Collections.ObjectModel;

namespace Mobappg4v2.Views;

public partial class ConfirmAddressView : ContentPage
{
	public ConfirmAddressView(ObservableCollection<ProductListModel> products, DeliveryTypeModel deliveryType)
	{
		InitializeComponent();
        BindingContext = new ConfirmAddressViewModel(products, deliveryType);
    }
}