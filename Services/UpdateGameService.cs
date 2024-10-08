using GameStore.Models;
using GameStore.Repository;
using GameStore.Shared.Errors.Game;
using GameStore.Shared.Responses;

namespace GameStore.Services;

public class UpdateGameService(IGameRepo gameRepo)
{
    public async Task<ResponseWrapper<Game>> Execute(Game game)
    {
        var resultGetById = await gameRepo.GetGameById(game.Id).ConfigureAwait(false);
        if (resultGetById.IsFailed)
        {
            return GameErrors.GameNotFound();
        }

        var resultUpdate = await gameRepo.UpdateGame(game.Id, game).ConfigureAwait(false);
        return resultUpdate;
    }
}