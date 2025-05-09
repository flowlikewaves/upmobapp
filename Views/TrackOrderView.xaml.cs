using Mobappg4v2.ViewModel;
using static Mobappg4v2.Model.TrackOrderModel;

namespace Mobappg4v2.Views;

public partial class TrackOrderView : ContentPage
{
    public TrackOrderView(Track data)
    {
        InitializeComponent();
        BindingContext = new TrackOrderViewModel(data);
    }
}