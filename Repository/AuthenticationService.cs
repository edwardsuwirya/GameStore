using GameStore.Models;
using GameStore.Shared.Helpers;
using GameStore.Shared.Services;

namespace GameStore.Repository;

public class AuthenticationService(IHttpService httpService) : IAuthenticationService
{
    public async Task<ResponseWrapper<Client>> Login(UserAccess userAccess)
    {
        try
        {
            var client = await httpService.Post<Client>("/api/users/authenticate", userAccess);
            return ResponseWrapper<Client>.Success(client);
        }
        catch (Exception e)
        {
            return ResponseWrapper<Client>.Fail("Access denied");
        }
    }
}