using GameStore.Models;
using GameStore.Shared.Helpers;

namespace GameStore.Repository;

public interface IAuthenticationService
{
    public Task<Client?> GetUserCredential();
    public Task<ResponseWrapper<int>> Login(UserAccess userAccess);
    public Task Logout();
}