using Mobappg4v2.Model;
using Mobappg4v2.ViewModel;
using System.Collections.ObjectModel;

namespace Mobappg4v2.Views;

public partial class FinishCartView : ContentPage
{
	public FinishCartView(ObservableCollection<ProductListModel> products, DeliveryTypeModel deliveryType, AddressModel address)
	{
		InitializeComponent();
		BindingContext = new FinishCartViewModel(products, deliveryType, address);

    }
}