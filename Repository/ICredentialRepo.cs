using GameStore.Models;
using GameStore.Shared.Helpers;
using GameStore.Shared.Responses;
using Refit;

namespace GameStore.Repository;

public interface ICredentialRepo
{
    [Post("/users/authenticate")]
    public Task<ResponseWrapper<Client>> Login([Body] UserAccess userAccess);
}