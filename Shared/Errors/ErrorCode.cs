namespace GameStore.Shared.Errors;

public enum ErrorCode
{
    Success = 0,
    Unauthorized = 4000,
    InsufficientPrivileges = 4001,
    TokenExpired = 4002,
    DataNotFound = 1000,
    DataRequired = 1001,
    Validation = 1002,
    GeneralError = 6000,
    PathNotFound = 6001
}