using GameStore.Data;
using GameStore.Models;

namespace GameStore.Repository;

public class GameService(GameDataSource dataSource) : IGameService
{
    public Game[] GetGames() => dataSource.games.ToArray();

    public void AddGame(Game game)
    {
        game.Id = dataSource.games.Max(g => g.Id) + 1;
        dataSource.games.Add(game);
    }

    public Game GetGameById(int id) =>
        dataSource.games.FirstOrDefault(g => g.Id == id) ?? throw new KeyNotFoundException();

    public void UpdateGame(Game updateGame)
    {
        var existingGame = GetGameById(updateGame.Id);
        existingGame.Name = updateGame.Name;
        existingGame.Genre = updateGame.Genre;
        existingGame.Price = updateGame.Price;
        existingGame.ReleaseDate = updateGame.ReleaseDate;
    }

    public void DeleteGame(int id)
    {
        var existingGame = GetGameById(id);
        dataSource.games.Remove(existingGame);
    }
}