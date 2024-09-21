using GameStore.Models;

namespace GameStore.Repository;

public interface IClientService
{
    public Client[] GetClient();
}