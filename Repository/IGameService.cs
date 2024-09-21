using GameStore.Models;

namespace GameStore.Repository;

public interface IGameService
{
    public Game[] GetGames();
    public void AddGame(Game game);
    public Game GetGameById(int id);
    public void UpdateGame(Game updateGame);
    public void DeleteGame(int id);
}