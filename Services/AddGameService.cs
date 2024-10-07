using GameStore.Models;
using GameStore.Repository;
using GameStore.Shared.Errors.Game;
using GameStore.Shared.Responses;

namespace GameStore.Services;

public class AddGameService(IGameRepo gameRepo)
{
    public Task<ResponseWrapper<Game>> Execute(Game game)
    {
        return gameRepo.AddGame(game);
    }
}