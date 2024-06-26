
namespace ToDoApp.MauiMobile.UsingRestApi;

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

        var asm = Assembly.GetExecutingAssembly();
        using var stream = asm.GetManifestResourceStream("ToDoApp.MauiMobile.UsingRestApi.appsettings.json");

        if (stream != null)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();

            // Register configuration
            builder.Configuration.AddConfiguration(configuration);
        }

        builder.Services.AddHttpClient<IRestDataService, RestDataService>();

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<MangeToDoPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
