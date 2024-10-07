using GameStore.Models;
using GameStore.Repository;
using GameStore.Shared.Responses;

namespace GameStore.Services;

public class GetClientListService(IClientRepo clientRepo)
{
    public Task<ResponseWrapper<Client[]>> Execute()
    {
        return clientRepo.GetClient();
    }
}