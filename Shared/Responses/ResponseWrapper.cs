namespace GameStore.Shared.Helpers;

// Result Pattern
public class ResponseWrapper<T>
{
    public ResponseStatus status { get; set; }

    public string Message { get; set; }

    public T? Data { get; set; }


    private ResponseWrapper(T data, string message = "Success")
    {
        status = ResponseStatus.Success;
        Message = message;
        Data = data;
    }

    private ResponseWrapper(string message)
    {
        status = ResponseStatus.Failed;
        Message = message;
        Data = default;
    }

    private ResponseWrapper()
    {
        status = ResponseStatus.Loading;
        Message = "";
        Data = default;
    }

    public bool IsLoading => status == ResponseStatus.Loading;
    public bool IsSuccess => status == ResponseStatus.Success;
    public bool IsFailed => status == ResponseStatus.Failed;


    public static ResponseWrapper<T> Success(T data, string message = "Success") => new(data, message);
    public static ResponseWrapper<T> Fail(string internalError) => new(internalError);
    public static ResponseWrapper<T> Loading() => new();
}