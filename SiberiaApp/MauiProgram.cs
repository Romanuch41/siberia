using Microsoft.Extensions.Logging;
using SiberiaApp.Classes.ViewModels;

namespace SiberiaApp
{
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
                    fonts.AddFont("Roboto-Italic-VariableFont_wdth_wght", "roboto");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddTransient<Authorization>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainMenuViewModels>();

            return builder.Build();
        }
    }
}
