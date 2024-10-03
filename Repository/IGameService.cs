using GameStore.Models;
using GameStore.Shared.Helpers;
using GameStore.Shared.Responses;

namespace GameStore.Repository;

public interface IGameService
{
    public Task<ResponseWrapper<Game[]>> GetGames();
    public Task<ResponseWrapper<Game>> AddGame(Game game);
    public Task<ResponseWrapper<Game>> GetGameById(int id);
    public Task<ResponseWrapper<Game>> UpdateGame(Game updateGame);
    public Task<ResponseWrapper<int>> DeleteGame(int id);
}