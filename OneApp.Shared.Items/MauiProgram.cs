﻿using OneApp.Shared.Items.ViewModels;

namespace OneApp.Shared.Items;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);

		builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<MainViewModel>();

		builder.Services.AddTransient<ListPage>();
		builder.Services.AddTransient<ListViewModel>();

		return builder.Build();
	}
}
