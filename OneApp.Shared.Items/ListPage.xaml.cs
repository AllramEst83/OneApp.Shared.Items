using OneApp.Shared.Items.ViewModels;

namespace OneApp.Shared.Items;

public partial class ListPage : ContentPage
{
	public ListPage(ListViewModel listViewModel)
	{

        InitializeComponent();
        BindingContext = listViewModel;
    }
}