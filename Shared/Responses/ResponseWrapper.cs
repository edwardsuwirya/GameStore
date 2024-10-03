using GameStore.Shared.Errors;

namespace GameStore.Shared.Responses;

// Result Pattern
public class ResponseWrapper<T>
{
    public ResponseStatus Status { get; set; }

    public AppError AppError { get; set; }

    public T? Data { get; set; }


    private ResponseWrapper(T data, string message = "Success")
    {
        Status = ResponseStatus.Success;
        AppError = AppError.None;
        Data = data;
    }

    private ResponseWrapper(AppError error, string message = "")
    {
        Status = ResponseStatus.Failed;
        AppError = error;
        Data = default;
    }

    public ResponseWrapper()
    {
        Status = ResponseStatus.Loading;
        AppError = AppError.None;
        Data = default;
    }

    public bool IsSuccess => Status == ResponseStatus.Success;
    public bool IsFailed => Status == ResponseStatus.Failed;


    public static ResponseWrapper<T> Success(T data, string message = "Success") => new(data, message);
    public static ResponseWrapper<T> Fail(AppError internalError) => new(internalError);
}