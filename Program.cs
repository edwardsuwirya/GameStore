using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GameStore;
using GameStore.Data;
using GameStore.Repository;
using GameStore.Shared.States;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();

void ConfigureCommonServices(IServiceCollection services)
{
    services.AddSingleton<ClientDataSource>();
    services.AddSingleton<GameDataSource>();
    services.AddScoped<IClientService, ClientService>();
    services.AddScoped<IGameService, GameService>();
    services.AddScoped<IAuthenticationService, AuthenticationService>();
}

void ConfigureStateServices(IServiceCollection services)
{
    services.AddScoped<AuthenticationStateProvider, AuthState>();
    services.AddScoped<LocalStorage>();
    services.AddScoped<LoadingState>();
}

ConfigureCommonServices(builder.Services);
ConfigureStateServices(builder.Services);
await builder.Build().RunAsync();