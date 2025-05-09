using Mobappg4v2.ViewModel;
namespace Mobappg4v2.Views;

public partial class WishListView : ContentPage
{   
    public WishListView()
	{
		InitializeComponent();
		BindingContext = new WishListViewModel();

    }
}