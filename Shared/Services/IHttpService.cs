using GameStore.Shared.Responses;

namespace GameStore.Shared.Services;

public interface IHttpService
{
    Task<ResponseWrapper<T>> Get<T>(string uri);
    Task<ResponseWrapper<T>> Post<T>(string uri, object value);

    Task<ResponseWrapper<T>> Put<T>(string uri, object value);

    Task<ResponseWrapper<T>> Delete<T>(string uri);
}