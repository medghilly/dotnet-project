using Microsoft.Extensions.Logging;
using IscaeAnnuaire.Data;
using IscaeAnnuaire.Views;

namespace IscaeAnnuaire
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
                });

            // Database
            builder.Services.AddSingleton<DatabaseService>();

            // Pages principales
            builder.Services.AddSingleton<AccueilPage>();
            builder.Services.AddSingleton<DepartementsPage>();
            builder.Services.AddSingleton<EnseignantsPage>();
            builder.Services.AddSingleton<EtudiantsPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
