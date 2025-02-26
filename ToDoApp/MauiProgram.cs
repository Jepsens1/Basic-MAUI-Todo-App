using Microsoft.Extensions.Logging;
using ToDoApp.Services;
using ToDoApp.ViewModels;

namespace ToDoApp;

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
		//Add required services, viewmodels and views to ServiceCollection
		builder.Services.AddSingleton<ITodoService, TodoService>();
		builder.Services.AddSingleton<MainPageViewModel>();
		builder.Services.AddSingleton<MainPage>();
#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
