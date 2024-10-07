using GameStore.Shared.Errors;

namespace GameStore.Shared.Responses;

// Result Pattern
public class ResponseWrapper<T>
{
    public ResponseStatus Status { get; set; }

    public AppError AppError { get; set; }

    public T? Data { get; set; }


    private ResponseWrapper(T data)
    {
        Status = ResponseStatus.Success;
        AppError = AppError.None;
        Data = data;
    }

    private ResponseWrapper(AppError error)
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


    public static ResponseWrapper<T> Success(T data) => new(data);
    public static ResponseWrapper<T> Fail(AppError internalError) => new(internalError);

    public static implicit operator ResponseWrapper<T>(T data) => new(data);
    public static implicit operator ResponseWrapper<T>(AppError internalError) => new(internalError);
}