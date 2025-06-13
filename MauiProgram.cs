using Microsoft.Extensions.Logging;
using ProyectoLite__8._0_.Services;
using ProyectoLite__8._0_.Pages;

namespace ProyectoLite__8._0_
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
        // Registrar DatabaseService como Singleton
        // Esto asegura que haya una única instancia de tu servicio de base de datos en toda la aplicación.
        builder.Services.AddSingleton<DatabaseService>();

            // Registrar las páginas que tienen dependencias en su constructor como Transient
            // Esto significa que se creará una nueva instancia de la página cada vez que se navegue a ella.
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<Principal>(); // Principal ahora recibe DatabaseService
            builder.Services.AddTransient<Producto>();   // Producto también recibe DatabaseService

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
