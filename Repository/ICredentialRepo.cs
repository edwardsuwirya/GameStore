using GameStore.Models;
using GameStore.Shared.Helpers;
using GameStore.Shared.Responses;

namespace GameStore.Repository;

public interface ICredentialRepo
{
    public Task<ResponseWrapper<Client>> Login(UserAccess userAccess);
}