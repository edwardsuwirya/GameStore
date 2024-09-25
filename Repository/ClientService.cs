using GameStore.Data;
using GameStore.Models;
using GameStore.Shared.Helpers;

namespace GameStore.Repository;

public class ClientService(ClientDataSource dataSource) : IClientService
{
    public async Task<ResponseWrapper<Client[]>> GetClient()
    {
        await Task.Delay(2000).ConfigureAwait(false);
        return ResponseWrapper<Client[]>.Success([]);
    }
}