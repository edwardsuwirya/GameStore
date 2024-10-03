using GameStore.Shared.Errors;

namespace GameStore.Shared.Responses;

public static class ResponseWrapperExt
{
    public static void Match<T>(
        this ResponseWrapper<T> result,
        Action<T> onSuccess,
        Action<AppError> onFailure)
    {
        if (result.IsSuccess)
        {
            onSuccess(result.Data);
        }
        else
        {
            onFailure(result.AppError);
        }
    }
}