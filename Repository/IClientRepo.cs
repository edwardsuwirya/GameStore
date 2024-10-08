using GameStore.Models;
using GameStore.Shared.Helpers;
using GameStore.Shared.Responses;
using Refit;

namespace GameStore.Repository;

public interface IClientRepo
{
    [Get("/clients")]
    public Task<ResponseWrapper<Client[]>> GetClient();
}