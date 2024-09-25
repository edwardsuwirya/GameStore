using GameStore.Models;
using GameStore.Shared.Helpers;

namespace GameStore.Repository;

public interface IClientService
{
    public Task<ResponseWrapper<Client[]>>  GetClient();
}