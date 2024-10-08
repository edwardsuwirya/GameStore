using GameStore.Models;
using GameStore.Shared.Helpers;
using GameStore.Shared.Responses;
using Refit;

namespace GameStore.Repository;

public interface IGameRepo
{
    [Get("/games")]
    public Task<ResponseWrapper<Game[]>> GetGames();

    [Post("/games")]
    public Task<ResponseWrapper<Game>> AddGame([Body] Game game);

    [Get("/games/{id}")]
    public Task<ResponseWrapper<Game>> GetGameById([AliasAs("id")] int id);

    [Put("/games/{id}")]
    public Task<ResponseWrapper<Game>> UpdateGame([AliasAs("id")] int id, [Body] Game updateGame);

    [Delete("/games/{id}")]
    public Task<ResponseWrapper<int>> DeleteGame([AliasAs("id")] int id);
}