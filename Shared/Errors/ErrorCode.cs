namespace GameStore.Shared.Errors;

public enum ErrorCode
{
    NoError = 0,
    UserAccessUnauthorized,
    UserAccessInvalidCredential,
    DataNotFound,
    DataRequired,
    AppError
}