using FoodDrinkApp.Services;
using Microsoft.Extensions.Logging;

namespace FoodDrinkApp;

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

        // Register database service as singleton
        builder.Services.AddSingleton<DatabaseService>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}