using GameStore.Repository;
using GameStore.Shared.Errors.Game;
using GameStore.Shared.Responses;

namespace GameStore.Services;

public class DeleteGameService(IGameRepo gameRepo)
{
    public async Task<ResponseWrapper<int>> Execute(int gameId)
    {
        var resultGetById = await gameRepo.GetGameById(gameId).ConfigureAwait(false);
        if (resultGetById.IsFailed)
        {
            return GameErrors.GameNotFound();
        }

        var resultDeletion = await gameRepo.DeleteGame(gameId).ConfigureAwait(false);
        return resultDeletion;
    }
}