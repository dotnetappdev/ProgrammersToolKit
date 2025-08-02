
using Microsoft.Extensions.DependencyInjection;
using ProgrammersToolKit.Data;
using ProgrammersToolKit.Services;

namespace ProgrammersToolKit
{
    public static class AppHost
    {
        public static ServiceProvider ServiceProvider { get; private set; }

        public static void ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>();
            services.AddScoped<IApiTestRepository, ApiTestRepository>();
            services.AddScoped<IApiTestRunner, ApiTestRunner>();
            services.AddScoped<ProgrammersToolKit.Services.Interfaces.IEncodingDecodingService, EncodingDecodingService>();
            services.AddScoped<ProgrammersToolKit.Services.Interfaces.ICodeRunnerService, CodeRunnerService>();
            services.AddScoped<ProgrammersToolKit.Services.Interfaces.ICookieInspectorService, CookieInspectorService>();
            services.AddScoped<ProgrammersToolKit.Services.Interfaces.IHexEditorService, HexEditorService>();
            services.AddScoped<ProgrammersToolKit.Services.Interfaces.IEncryptionToolService, EncryptionToolService>();
            services.AddScoped<ProgrammersToolKit.Services.Interfaces.IHeaderInspectorService, HeaderInspectorService>();
            ServiceProvider = services.BuildServiceProvider();

            // Ensure SQLite folder exists and apply migrations
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dir = Path.Combine(appData, "ProgrammersToolKit");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            using (var scope = ServiceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.Migrate();
            }
        }
    }
}
