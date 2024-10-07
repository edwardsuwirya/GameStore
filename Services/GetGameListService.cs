using GameStore.Models;
using GameStore.Repository;
using GameStore.Shared.Responses;

namespace GameStore.Services;

public class GetGameListService(IGameRepo gameRepo)
{
    public Task<ResponseWrapper<Game[]>> Execute()
    {
        return gameRepo.GetGames();
    }
}