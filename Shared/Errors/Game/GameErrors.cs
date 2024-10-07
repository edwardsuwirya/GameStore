namespace GameStore.Shared.Errors.Game;

public static class GameErrors
{
    public static AppError GameNotFound(string desc = "Game is not found") => new(
        ErrorCode.DataNotFound, desc);
}