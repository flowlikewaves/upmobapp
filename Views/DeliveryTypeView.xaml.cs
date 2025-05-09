using Mobappg4v2.Model;
using Mobappg4v2.ViewModel;
using System.Collections.ObjectModel;

namespace Mobappg4v2.Views;

public partial class DeliveryTypeView : ContentPage
{
	public DeliveryTypeView(ObservableCollection<ProductListModel> products)
	{
		InitializeComponent();
        BindingContext = new DeliveryTypeViewModel(products);
    }
}