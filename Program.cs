using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GameStore;
using GameStore.Data;
using GameStore.Repository;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

void ConfigureCommonServices(IServiceCollection services)
{
    services.AddSingleton<ClientDataSource>();
    services.AddSingleton<GameDataSource>();
    services.AddScoped<IClientService, ClientService>();
    services.AddScoped<IGameService, GameService>();
}

ConfigureCommonServices(builder.Services);
await builder.Build().RunAsync();