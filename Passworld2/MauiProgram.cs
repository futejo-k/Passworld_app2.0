using Microsoft.Extensions.Logging;
using Passworld.Data;
using Passworld.ViewModels;

namespace Passworld2
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
                    fonts.AddFont("SpaceGrotesk-Regular.ttf", "SpaceGroteskRegular");
                    fonts.AddFont("SpaceGrotesk-Light.ttf", "SpaceGroteskLight");
                    fonts.AddFont("SpaceGrotesk-Medium.ttf", "SpaceGroteskMedium");
                    fonts.AddFont("SpaceGrotesk-SemiBold.ttf", "SpaceGroteskSemiBold");
                    fonts.AddFont("SpaceGrotesk-Bold.ttf", "SpaceGroteskBold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<DatabaseContext>();
            builder.Services.AddSingleton<PasswordViewModel>();
            builder.Services.AddSingleton<MainPage>();

            return builder.Build();
        }
    }
}
