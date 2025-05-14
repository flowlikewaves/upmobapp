using Mobappg4v2.ViewModel;
using Mobappg4v2.Model;

namespace Mobappg4v2.Views;

public partial class OrderDetailsView : ContentPage
{
    public OrderDetailsView()
    {
        InitializeComponent();
        BindingContext = new OrderDetailsViewModel();
    }
}