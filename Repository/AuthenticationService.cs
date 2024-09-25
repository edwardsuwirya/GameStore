using GameStore.Models;
using GameStore.Shared.Helpers;
using GameStore.Shared.States;

namespace GameStore.Repository;

public class AuthenticationService(LocalStorageService localStorageService) : IAuthenticationService
{
    public async Task<Client> GetUserCredential()
    {
        var token = await localStorageService.GetToken("auth-token");
        var client = Serialization.Deserialize<Client>(token);
        return client;
    }

    public async Task<ResponseWrapper<int>> Login(UserAccess userAccess)
    {
        await Task.Delay(1000).ConfigureAwait(false);
        if (!userAccess.UserName.Equals("edo") || !userAccess.Password.Equals("123456"))
            return ResponseWrapper<int>.Fail("Access denied");

        var user = new Client
        {
            Id = 1,
            FirstName = "Edo",
            LastName = "",
            Address = "123 Main Street",
            Email = "edo@gmail.com",
            Phone = "123456"
        };
        // authState.Client = client;
        var serializedToken = Serialization.Serialize(user);
        await localStorageService.SetToken(serializedToken, "auth-token");
        return ResponseWrapper<int>.Success(user.Id);
    }

    public async Task Logout()
    {
        await localStorageService.RemoveToken("auth-token");
    }
}