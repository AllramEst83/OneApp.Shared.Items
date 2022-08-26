using OneApp.Shared.Items.ViewModels;

namespace OneApp.Shared.Items.Views;

public partial class ListInfoPage : ContentPage
{
	public ListInfoPage(ListInfoViewmodel listInfoViewmodel)
	{
		InitializeComponent();

        InitializeComponent();
        BindingContext = listInfoViewmodel;
    }
}