using GameStore.Shared.Navigation;
using GameStore.Shared.States;
using Microsoft.AspNetCore.Components;

namespace GameStore.Pages.Authentication;

public partial class Logout : ComponentBase
{
    protected override async Task OnInitializedAsync()
    {
        var authStateProvider = (AuthState)AuthStateProvider;
        await authStateProvider.UpdateAuthenticationState(null);
        NavManager.NavigateTo(PageRoute.Login, forceLoad: true);
    }
}