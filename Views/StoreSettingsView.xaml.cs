using Mobappg4v2.ViewModel;

namespace Mobappg4v2.Views
{
    public partial class StoreSettingsView : ContentPage
    {
        public StoreSettingsView()
        {
            InitializeComponent();
            BindingContext = new StoreSettingsViewModel();
        }
    }
} 