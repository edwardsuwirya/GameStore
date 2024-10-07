using Blazored.LocalStorage;
using GameStore;
using GameStore.Repository;
using GameStore.Services;
using GameStore.Shared.Helpers;
using GameStore.Shared.Services;
using GameStore.Shared.States;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();

void ConfigureRepositories(IServiceCollection services)
{
    services.AddScoped<IClientRepo, ClientRepo>();
    services.AddScoped<IGameRepo, GameRepo>();
    services.AddScoped<ICredentialRepo, CredentialRepo>();
}

void ConfigureDomainServices(IServiceCollection services)
{
    services.AddScoped<LoginService>();
    services.AddScoped<LogoutService>();
    
    services.AddScoped<GetClientListService>();

    services.AddScoped<DeleteGameService>();
    services.AddScoped<UpdateGameService>();
    services.AddScoped<AddGameService>();
    services.AddScoped<GetGameListService>();
    services.AddScoped<GetGameService>();
}

void ConfigureCommonServices(IServiceCollection services)
{
    services.AddScoped<IHttpService, HttpService>();

    services.AddScoped(x =>
    {
        var apiUrl = new Uri(builder.Configuration["apiUrl"]);
        Console.WriteLine($"API URL: {apiUrl}");
        if (builder.Configuration["fakeBackend"] != "true") return new HttpClient { BaseAddress = apiUrl };
        var fakeBackendHandler = new FakeBackendHandler();
        return new HttpClient(fakeBackendHandler) { BaseAddress = apiUrl };
    });
}

void ConfigureStateServices(IServiceCollection services)
{
    services.AddScoped<AuthenticationStateProvider, AuthState>();
    services.AddScoped<LocalStorage>();
    services.AddScoped<LoadingState>();
}

ConfigureCommonServices(builder.Services);
ConfigureRepositories(builder.Services);
ConfigureDomainServices(builder.Services);
ConfigureStateServices(builder.Services);
await builder.Build().RunAsync();