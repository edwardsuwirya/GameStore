namespace GameStore.Shared.Errors;

public record AppError(ErrorCode Code, string Description)
{
    public static AppError None = new(ErrorCode.NoError, string.Empty);

    public static AppError UnauthorizedUser(string desc = "Unauthorized user") => new(
        ErrorCode.UserAccessUnauthorized, desc);

    public static AppError InvalidUser(string desc = "Invalid user name & password") => new(
        ErrorCode.UserAccessInvalidCredential, desc);

    public static AppError DataNotFound(string desc = "Data is not found") => new(
        ErrorCode.DataNotFound, desc);

    public static AppError DataRequired(string desc = "Data is required") => new(
        ErrorCode.DataRequired, desc);

    public static AppError GeneralError(string desc = "General Error") => new(
        ErrorCode.AppError, desc);
}