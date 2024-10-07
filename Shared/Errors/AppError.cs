namespace GameStore.Shared.Errors;

public record AppError(ErrorCode Code, string Description)
{
    public static AppError None = new(ErrorCode.Success, string.Empty);

    public static AppError PathError(string desc = "Path not found") => new(
        ErrorCode.PathNotFound, desc);

    public static AppError GeneralError(string desc = "General Error") => new(
        ErrorCode.GeneralError, desc);
}