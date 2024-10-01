using GameStore.Dtos;
using GameStore.Models;
using GameStore.Shared.Helpers;
using GameStore.Shared.Navigation;
using GameStore.Shared.States;
using Microsoft.AspNetCore.Components;

namespace GameStore.Pages.Authentication;

public partial class Login : ComponentBase
{
    private UserAccess userAccessCredential;
    private bool isLoading;
    private string error;

    protected override void OnInitialized()
    {
        CreateEmptyUserAccessCredential();
    }

    private void CreateEmptyUserAccessCredential()
    {
        userAccessCredential = new UserAccess
        {
            UserName = string.Empty,
            Password = string.Empty,
        };
    }

    private async void HandleSubmit()
    {
        error = string.Empty;
        isLoading = true;
        var loginResponse = await AuthenticationService.Login(userAccessCredential);
        switch (loginResponse.status)
        {
            case ResponseStatus.Loading:
                break;
            case ResponseStatus.Success:
                var authStateProvider = (AuthState)AuthStateProvider;
                await authStateProvider.UpdateAuthenticationState(loginResponse.Data.Token);
                NavManager.NavigateTo(PageRoute.Game, forceLoad: true);
                break;
            case ResponseStatus.Failed:
                isLoading = false;
                error = loginResponse.Message;
                CreateEmptyUserAccessCredential();
                StateHasChanged();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}