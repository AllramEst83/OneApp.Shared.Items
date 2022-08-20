using OneApp.Shared.Items.ViewModels;

namespace OneApp.Shared.Items;

public partial class MainPage : ContentPage
{

    public MainPage(MainViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }


}

