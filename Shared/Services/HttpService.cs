using System.Net.Http.Json;
using System.Text;
using GameStore.Shared.Helpers;

namespace GameStore.Shared.Services;

public class HttpService(HttpClient httpClient) : IHttpService
{
    public async Task<T> Get<T>(string uri)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        return await sendRequest<T>(request);
    }

    public async Task<T> Post<T>(string uri, object value)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, uri);
        request.Content = new StringContent(Serialization.Serialize(value), Encoding.UTF8, "application/json");
        return await sendRequest<T>(request);
    }

    private async Task<T> sendRequest<T>(HttpRequestMessage request)
    {
        using var response = await httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<T>();
        var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
        throw new Exception(error["message"]);
    }
}