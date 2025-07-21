using Microsoft.Extensions.Logging;
#if WINDOWS
using CommunityToolkit.Maui.Maps;
#endif

namespace ContactMap.Presentation.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()

#if WINDOWS
            // Initialize the .NET MAUI Community Toolkit Maps by adding the below line of code
            .UseMauiCommunityToolkitMaps("<BINGkey>") // TODO: Replace "key" with your actual BING API key or wait for MAUI to implement maps for windows
#else
            // For all other platforms
            .UseMauiMaps()
#endif
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}
