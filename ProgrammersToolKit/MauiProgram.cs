using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ProgrammersToolKit.Data;
using ProgrammersToolKit.Services;

namespace ProgrammersToolKit;

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
            });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        // Register Entity Framework
        builder.Services.AddDbContext<ApplicationDbContext>();

        // Register services from original AppHost
        builder.Services.AddScoped<IApiTestRepository, ApiTestRepository>();
        builder.Services.AddScoped<IApiTestRunner, ApiTestRunner>();
        builder.Services.AddScoped<ProgrammersToolKit.Services.Interfaces.IEncodingDecodingService, EncodingDecodingService>();
        builder.Services.AddScoped<ICodeRunnerService, CodeRunnerService>();
        builder.Services.AddScoped<ICookieInspectorService, CookieInspectorService>();
        builder.Services.AddScoped<IHexEditorService, HexEditorService>();
        builder.Services.AddScoped<ProgrammersToolKit.Services.Interfaces.IEncryptionToolService, EncryptionToolService>();
        builder.Services.AddScoped<ProgrammersToolKit.Services.Interfaces.IHeaderInspectorService, HeaderInspectorService>();

        var app = builder.Build();

        // Ensure SQLite folder exists and apply migrations
        using (var scope = app.Services.CreateScope())
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dir = Path.Combine(appData, "ProgrammersToolKit");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            db.Database.Migrate();
        }

        return app;
    }
}