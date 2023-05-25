using NetflixClone.Services;
using NetflixClone.ViewModels;
using NetflixClone.Views;

namespace NetflixClone;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("Poppins-Regular.ttf", "poppinsRegular");
				fonts.AddFont("Poppins-Semibold.ttf", "poppinsSemibold");
                fonts.AddFont("Poppins-Bold.ttf", "poppinsBold");
            });


		builder.Services.AddHttpClient(TmdbService.httpClientName, httpClient => httpClient.BaseAddress = new Uri("https://api.themoviedb.org/"));
		builder.Services.AddSingleton<TmdbService>();
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<HomeViewModel>();

        return builder.Build();

		
	}
}
