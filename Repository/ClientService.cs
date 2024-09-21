using GameStore.Data;
using GameStore.Models;

namespace GameStore.Repository;

public class ClientService(ClientDataSource dataSource) : IClientService
{
    public Client[] GetClient()
    {
        return [];
    }
}