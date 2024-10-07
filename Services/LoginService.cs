using GameStore.Models;
using GameStore.Repository;
using GameStore.Shared.Errors;
using GameStore.Shared.Errors.Auth;
using GameStore.Shared.Responses;
using GameStore.Shared.States;
using Microsoft.AspNetCore.Components.Authorization;

namespace GameStore.Services;

public class LoginService(ICredentialRepo userCredentialRepo, AuthenticationStateProvider authStateProvider)
{
    public async Task<ResponseWrapper<bool>> Execute(UserAccess userAccess)
    {
        var result = await userCredentialRepo.Login(userAccess).ConfigureAwait(false);
        if (result.IsFailed)
        {
            return result.AppError.Code == ErrorCode.Unauthorized
                ? AuthErrors.UnauthorizedUser()
                : result.AppError;
        }

        var provider = (AuthState)authStateProvider;
        await provider.UpdateAuthenticationState(result.Data).ConfigureAwait(false);
        return true;
    }
}