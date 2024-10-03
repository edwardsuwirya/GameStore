using GameStore.Models;
using GameStore.Shared.Helpers;
using GameStore.Shared.Responses;

namespace GameStore.Repository;

public interface IClientService
{
    public Task<ResponseWrapper<Client[]>>  GetClient();
}