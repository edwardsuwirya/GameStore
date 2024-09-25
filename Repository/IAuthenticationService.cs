using GameStore.Models;
using GameStore.Shared.Helpers;

namespace GameStore.Repository;

public interface IAuthenticationService
{
    public Task<ResponseWrapper<Client>> Login(UserAccess userAccess);
}