using Mobappg4v2.Model;
using Mobappg4v2.ViewModel;

namespace Mobappg4v2.Views;

public partial class CategoryDetailView : ContentPage
{
    public CategoryDetailView(CategoriesModel data)
    {
        InitializeComponent();
        BindingContext = new CategoryDetailViewModel(data);
    }
}