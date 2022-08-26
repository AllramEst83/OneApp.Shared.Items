using OneApp.Shared.Items.ViewModels;

namespace OneApp.Shared.Items.Views;

public partial class EditListItemPage : ContentPage
{
	public EditListItemPage(EditListItemViewModel editListItemViewModel)
	{
		InitializeComponent();
		BindingContext = editListItemViewModel;
	}
}