using OneApp.Shared.Items.Views;

namespace OneApp.Shared.Items;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(MainPage),typeof(MainPage));
        Routing.RegisterRoute(nameof(ListPage), typeof(ListPage));
        Routing.RegisterRoute(nameof(ListInfoPage), typeof(ListInfoPage));
    }
}
