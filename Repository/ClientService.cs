using GameStore.Models;
using GameStore.Shared.Responses;
using GameStore.Shared.Services;

namespace GameStore.Repository;

public class ClientService(IHttpService httpService) : IClientService
{
    private const string BasePath = "/api/v1/clients";

    public Task<ResponseWrapper<Client[]>> GetClient() =>
        httpService.Get<Client[]>(BasePath);
}