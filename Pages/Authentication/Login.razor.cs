using GameStore.Models;
using Microsoft.AspNetCore.Components;

namespace GameStore.Pages.Authentication;

public partial class Login : ComponentBase
{
    private UserAccess? _userAccessCredential;

    protected override void OnInitialized()
    {
        _userAccessCredential = new UserAccess()
        {
            UserName = string.Empty,
            Password = string.Empty,
        };
    }

    private void HandleSubmit()
    {
        var client = AccountService.Login(_userAccessCredential);
        NavManager.NavigateTo(client == null ? "/Authentication/Login" : "/Game");
    }
}