using GameStore.Models;
using GameStore.Shared.Errors;
using GameStore.Shared.Navigation;
using GameStore.Shared.Responses;
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
        var client = await LoginService.Execute(userAccessCredential);
        client.Match(onSuccess: onSuccess, onFailure: onFailure);
    }

    private void onFailure(AppError errorMessage)
    {
        if (errorMessage.Code is ErrorCode.GeneralError or ErrorCode.PathNotFound)
        {
            NavManager.NavigateTo(PageRoute.Error, forceLoad: true);
        }

        isLoading = false;
        error = errorMessage.Description;
        CreateEmptyUserAccessCredential();
        StateHasChanged();
    }

    private void onSuccess(bool success)
    {
        isLoading = false;
        NavManager.NavigateTo(PageRoute.Game, forceLoad: true);
    }
}