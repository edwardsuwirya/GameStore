using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GameStore;
using GameStore.Data;
using GameStore.Repository;
using GameStore.Shared.States;

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
    services.AddScoped<IAccountService, AccountService>();
}

void ConfigureStateServices(IServiceCollection services)
{
    services.AddSingleton<AuthState>();
}

ConfigureCommonServices(builder.Services);
ConfigureStateServices(builder.Services);
await builder.Build().RunAsync();