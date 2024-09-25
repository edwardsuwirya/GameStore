using GameStore.Shared.Navigation;
using Microsoft.AspNetCore.Components;

namespace GameStore.Pages.Authentication;

public partial class Logout : ComponentBase
{
    protected override async Task OnInitializedAsync()
    {
        await AuthState.UpdateAuthenticationState(null);
        NavManager.NavigateTo(PageRoute.Login, forceLoad: true);
    }
}