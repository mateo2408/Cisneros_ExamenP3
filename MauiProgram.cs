using Cisneros_ExamenP3.Repository;
using Cisneros_ExamenP3.ViewModels;
using Cisneros_ExamenP3.Pages;

namespace Cisneros_ExamenP3;

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

        // Register services for dependency injection
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "clientes.db");
        builder.Services.AddSingleton(provider => new ClienteRepository(dbPath, "Cisneros"));
        builder.Services.AddTransient<ClienteViewModel>();
        
        // Register pages
        builder.Services.AddTransient<RegistroPage>();
        builder.Services.AddTransient<ClientesPage>();
        builder.Services.AddTransient<LogsPage>();
        builder.Services.AddTransient<MainPage>();


        return builder.Build();
    }
}