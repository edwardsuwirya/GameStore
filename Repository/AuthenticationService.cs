using GameStore.Models;
using GameStore.Shared.Responses;
using GameStore.Shared.Services;

namespace GameStore.Repository;

public class AuthenticationService(IHttpService httpService) : IAuthenticationService
{
    private const string BasePath = "/api/v1/users/authenticate";

    public async Task<ResponseWrapper<Client>> Login(UserAccess userAccess)
    {
        return await httpService.Post<Client>(BasePath, userAccess);
    }
}