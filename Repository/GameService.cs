using GameStore.Models;
using GameStore.Shared.Responses;
using GameStore.Shared.Services;

namespace GameStore.Repository;

public class GameService(IHttpService httpService) : IGameService
{
    private const string BasePath = "/api/v1/games";

    public Task<ResponseWrapper<Game[]>> GetGames() =>
        httpService.Get<Game[]>(BasePath);

    public Task<ResponseWrapper<Game>> AddGame(Game game) =>
        httpService.Post<Game>(BasePath, game);

    public Task<ResponseWrapper<Game>> GetGameById(int id) =>
        httpService.Get<Game>($"{BasePath}/{id}");


    public Task<ResponseWrapper<Game>> UpdateGame(Game updateGame) =>
        httpService.Put<Game>($"{BasePath}/{updateGame.Id}", updateGame);

    public Task<ResponseWrapper<int>> DeleteGame(int id) =>
        httpService.Delete<int>($"{BasePath}/{id}");
}