using Microsoft.AspNetCore.Components;

namespace GameStore.Pages.Authentication;

public partial class Login : ComponentBase
{
    private Models.UserAccess? userAccessCredential;

    protected override void OnInitialized()
    {
        userAccessCredential = new Models.UserAccess()
        {
            UserName = string.Empty,
            Password = string.Empty,
        };
    }

    private void HandleSubmit()
    {
        if (userAccessCredential == null)
        {
            NavManager.NavigateTo("/Authentication/Login");
        }
        else
        {
            if (userAccessCredential.UserName.Equals("edo") && userAccessCredential.Password.Equals("123456"))
            {
                NavManager.NavigateTo("/Game");
            }
        }
    }
}