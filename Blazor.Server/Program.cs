using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

namespace Blazor.Server;

public static class Program
{
    static WebAssemblyHost AssemblyHost { get; set; }
    public static WebAssemblyHostBuilder Builder { get; private set; }

    public static async Task Main(string[] args)
    {
        Builder = WebAssemblyHostBuilder.CreateDefault(args);
        Builder.RootComponents.Add<App>("#app");
        Builder.RootComponents.Add<HeadOutlet>("head::after");

        Builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(Builder.HostEnvironment.BaseAddress) });
        Builder.Services.AddMudServices();
        /*    Builder.Services.AddScoped<IServiceManager, ServiceManager>();
    
            Builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
    
            Builder.Services.AddDbContextPool<RepositoryDbContext>(builder =>
            {
                var connectionString = Configuration.GetConnectionString("Database");
    
                builder.UseSqlServer(connectionString);
            });*/

        AssemblyHost = Builder.Build();
        await AssemblyHost.RunAsync();
    }
}