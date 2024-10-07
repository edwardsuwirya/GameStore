using GameStore.Models;
using GameStore.Repository;
using GameStore.Shared.Responses;

namespace GameStore.Services;

public class GetGameService(IGameRepo gameRepo)
{
    public Task<ResponseWrapper<Game>> Execute(int id)
    {
        return gameRepo.GetGameById(id);
    }
}