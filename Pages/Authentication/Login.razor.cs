using GameStore.Models;
using GameStore.Shared.Helpers;
using GameStore.Shared.Navigation;
using Microsoft.AspNetCore.Components;

namespace GameStore.Pages.Authentication;

public partial class Login : ComponentBase
{
    private UserAccess userAccessCredential;
    private bool isLoading;

    protected override void OnInitialized()
    {
        userAccessCredential = new UserAccess
        {
            UserName = string.Empty,
            Password = string.Empty,
        };
    }

    private async void HandleSubmit()
    {
        isLoading = true;
        await Task.Delay(1000);
        var client = await AuthenticationService.Login(userAccessCredential);
        switch (client.status)
        {
            case ResponseStatus.Loading:
                break;
            case ResponseStatus.Success:
                isLoading = false;
                NavManager.NavigateTo(PageRoute.Game, forceLoad: true);
                break;
            case ResponseStatus.Failed:
                isLoading = false;
                // NavManager.NavigateTo(PageRoute.Login);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}