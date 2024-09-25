using GameStore.Shared.Helpers;
using Microsoft.AspNetCore.Components;

namespace GameStore.Layout;

public partial class ProtectedView : ComponentBase
{
    [Parameter] public RenderFragment<string> AuthorizedView { get; set; }
    [Parameter] public RenderFragment UnauthorizedView { get; set; }
    [Parameter] public string StorageKey { get; set; } = string.Empty;

    private ResponseWrapper<string> userInfoResponse = ResponseWrapper<string>.Loading();

    protected override async Task OnInitializedAsync()
    {
        var user = await AuthState.GetAuthenticationStateAsync();
        if (user is null)
        {
            userInfoResponse = ResponseWrapper<string>.Fail("Access denied");
        }
        else
        {
            try
            {
                var userInfo = user.FirstName + "" + user.LastName;
                userInfoResponse = ResponseWrapper<string>.Success(userInfo);
            }
            catch (Exception)
            {
                userInfoResponse = ResponseWrapper<string>.Fail("Access denied");
            }
        }
    }
}