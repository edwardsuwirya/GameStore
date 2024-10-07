namespace GameStore.Shared.Errors.Auth;

public static class AuthErrors
{
    public static AppError UnauthorizedUser(string desc = "Invalid user name & password") => new(
        ErrorCode.Unauthorized, desc);

    public static AppError TokenExpired(string desc = "Token expired") => new(
        ErrorCode.TokenExpired, desc);

    public static AppError InsufficientPrivileges(string desc = "Not enough privileges") => new(
        ErrorCode.InsufficientPrivileges, desc);
}