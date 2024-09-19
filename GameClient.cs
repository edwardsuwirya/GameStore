using GameStore.Models;

namespace GameStore;

public static class GameClient
{
    private static readonly List<Game> games =
    [
        new()
        {
            Id = 1,
            Name = "Street Fighter II",
            Genre = "Fighting",
            Price = 19.99M,
            ReleaseDate = new DateTime(1991, 1, 23)
        },

        new()
        {
            Id = 2,
            Name = "Final Fantasy XIV",
            Genre = "Role Playing",
            Price = 59.99M,
            ReleaseDate = new DateTime(2010, 9, 30)
        },

        new()
        {
            Id = 3,
            Name = "FIFA 23",
            Genre = "Sports",
            Price = 69.99M,
            ReleaseDate = new DateTime(2022, 9, 2)
        }
    ];

    public static Game[] GetGames() => games.ToArray();

    public static void AddGame(Game game)
    {
        game.Id = games.Max(g => g.Id) + 1;
        games.Add(game);
    }

    public static Game GetGameById(int id) => games.FirstOrDefault(g => g.Id == id) ?? throw new KeyNotFoundException();

    public static void UpdateGame(Game updateGame)
    {
        var existingGame = GetGameById(updateGame.Id);
        existingGame.Name = updateGame.Name;
        existingGame.Genre = updateGame.Genre;
        existingGame.Price = updateGame.Price;
        existingGame.ReleaseDate = updateGame.ReleaseDate;
    }

    public static void DeleteGame(int id)
    {
        var existingGame = GetGameById(id);
        games.Remove(existingGame);
    }
}