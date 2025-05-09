using Mobappg4v2.Model;
using Mobappg4v2.ViewModel;
using System.Collections.ObjectModel;

namespace Mobappg4v2.Views;

public partial class CartCalculation : ContentPage
{
	public CartCalculation(ObservableCollection<ProductListModel> ProductList)
	{
		InitializeComponent();
        BindingContext = new CartCalculationViewModel(ProductList);
    }
}