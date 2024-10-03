using System.Net.Http.Json;
using System.Text;
using GameStore.Shared.Errors;
using GameStore.Shared.Helpers;
using GameStore.Shared.Responses;

namespace GameStore.Shared.Services;

public class HttpService(HttpClient httpClient) : IHttpService
{
    public async Task<ResponseWrapper<T>> Get<T>(string uri)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        return await SendRequest<T>(request);
    }

    public async Task<ResponseWrapper<T>> Post<T>(string uri, object value)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, uri);
        request.Content = new StringContent(Serialization.Serialize(value), Encoding.UTF8, "application/json");
        return await SendRequest<T>(request);
    }

    public async Task<ResponseWrapper<T>> Put<T>(string uri, object value)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, uri);
        request.Content = new StringContent(Serialization.Serialize(value), Encoding.UTF8, "application/json");
        return await SendRequest<T>(request);
    }

    public async Task<ResponseWrapper<T>> Delete<T>(string uri)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, uri);
        return await SendRequest<T>(request);
    }

    private async Task<ResponseWrapper<T>> SendRequest<T>(HttpRequestMessage request)
    {
        try
        {
            using var response = await httpClient.SendAsync(request);
            var result = await response.Content.ReadFromJsonAsync<ResponseWrapper<T>>();
            if (result.IsFailed)
            {
                Console.WriteLine(result.AppError.Description);
            }

            return result;
        }
        catch (Exception e)
        {
            return ResponseWrapper<T>.Fail(AppError.GeneralError(e.Message));
        }
    }
}