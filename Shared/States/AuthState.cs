using GameStore.Models;
using GameStore.Shared.Helpers;

namespace GameStore.Shared.States;

public class AuthState(LocalStorage localStorage)
{
    public async Task<Client> GetAuthenticationStateAsync()
    {
        var token = await localStorage.GetToken("auth-token");
        var client = Serialization.Deserialize<Client>(token);
        return client;
    }

    public async Task UpdateAuthenticationState(Client client)
    {
        if (client != null)
        {
            var serializedToken = Serialization.Serialize(client);
            await localStorage.SetToken(serializedToken, "auth-token");
        }
        else
        {
            await localStorage.RemoveToken("auth-token");
        }
    }
}