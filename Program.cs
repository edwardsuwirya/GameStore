using Blazored.LocalStorage;
using GameStore;
using GameStore.Repository;
using GameStore.Services;
using GameStore.Shared.Helpers;
using GameStore.Shared.States;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();

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

void ConfigureApiServices(IServiceCollection services)
{
    // services.AddScoped<IHttpService, HttpService>();
    var apiUrl = new Uri(builder.Configuration["apiUrl"]);
    Console.WriteLine($"API URL: {apiUrl}");
    Action<HttpClient> httpClient = (c) => { c.BaseAddress = apiUrl; };

    var gameRepo = services.AddRefitClient<IGameRepo>().ConfigureHttpClient(httpClient);
    var credentialRepo = services.AddRefitClient<ICredentialRepo>().ConfigureHttpClient(httpClient);
    var clientRepo = services.AddRefitClient<IClientRepo>().ConfigureHttpClient(httpClient);

    if (builder.Configuration["fakeBackend"] != "true") return;

    services.AddTransient<FakeBackendHandler>();
    gameRepo.AddHttpMessageHandler<FakeBackendHandler>();
    credentialRepo.AddHttpMessageHandler<FakeBackendHandler>();
    clientRepo.AddHttpMessageHandler<FakeBackendHandler>();
}

void ConfigureStateServices(IServiceCollection services)
{
    services.AddScoped<AuthenticationStateProvider, AuthState>();
    services.AddScoped<LocalStorage>();
    services.AddScoped<LoadingState>();
}

ConfigureApiServices(builder.Services);
ConfigureDomainServices(builder.Services);
ConfigureStateServices(builder.Services);
await builder.Build().RunAsync();