using GameStore.Shared.Responses;
using GameStore.Shared.States;
using Microsoft.AspNetCore.Components.Authorization;

namespace GameStore.Services;

public class LogoutService(AuthenticationStateProvider authStateProvider)
{
    public async Task<ResponseWrapper<bool>> Execute()
    {
        var provider = (AuthState)authStateProvider;
        await provider.UpdateAuthenticationState(null);
        return true;
    }
}