using GameStore.Data;
using GameStore.Models;
using GameStore.Shared.Helpers;

namespace GameStore.Repository;

public class GameService(GameDataSource dataSource) : IGameService
{
    public async Task<ResponseWrapper<Game[]>> GetGames()
    {
        await Task.Delay(2000);
        var games = dataSource.games.ToArray();
        return await Task.FromResult(ResponseWrapper<Game[]>.Success(games));
    }

    public async Task<ResponseWrapper<Game>> AddGame(Game game)
    {
        await Task.Delay(2000);
        game.Id = dataSource.games.Max(g => g.Id) + 1;
        dataSource.games.Add(game);
        return await Task.FromResult(ResponseWrapper<Game>.Success(game));
    }

    public async Task<ResponseWrapper<Game>> GetGameById(int id)
    {
        await Task.Delay(2000);
        var result = dataSource.games.FirstOrDefault(g => g.Id == id);
        return await Task.FromResult(result == null
            ? ResponseWrapper<Game>.Fail("Game not found")
            : ResponseWrapper<Game>.Success(result));
    }


    public async Task<ResponseWrapper<Game>> UpdateGame(Game updateGame)
    {
        var existingGame = await GetGameById(updateGame.Id);
        if (existingGame.IsFailed)
            return await Task.FromResult(ResponseWrapper<Game>.Fail("Game not found"));
        existingGame.Data.Name = updateGame.Name;
        existingGame.Data.Genre = updateGame.Genre;
        existingGame.Data.Price = updateGame.Price;
        existingGame.Data.ReleaseDate = updateGame.ReleaseDate;
        return await Task.FromResult(ResponseWrapper<Game>.Success(existingGame.Data));
    }

    public async Task<ResponseWrapper<int>> DeleteGame(int id)
    {
        Console.WriteLine("Loading");
        var existingGame = await GetGameById(id);
        if (existingGame.IsFailed)
        {
            Console.WriteLine("Failed");
            return await Task.FromResult(ResponseWrapper<int>.Fail("Game not found"));
        }

        Console.WriteLine("success");
        dataSource.games.Remove(existingGame.Data);
        return await Task.FromResult(ResponseWrapper<int>.Success(existingGame.Data.Id));
    }
}