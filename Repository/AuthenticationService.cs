using GameStore.Models;
using GameStore.Shared.Helpers;
using GameStore.Shared.Services;
using Blazored.LocalStorage;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GameStore.Repository;

public class AuthenticationService(IHttpService httpService, ILocalStorageService localStorage) : IAuthenticationService
{
    public async Task<ResponseWrapper<Client>> Login(UserAccess userAccess)
    {
        try
        {
            var response = await httpService.Post<LoginResponse>("/api/users/authenticate", userAccess);
            if (response.Token != null)
            {
                await localStorage.SetItemAsync("authToken", response.Token);
                var client = DecodeToken(response.Token);
                return ResponseWrapper<Client>.Success(client);

            }
            return ResponseWrapper<Client>.Fail("Invalid response from server");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Access denied. Exception: {e.Message}");
            return ResponseWrapper<Client>.Fail("Access denied");
        }
    }

    public async Task Logout()
    {
        await localStorage.RemoveItemAsync("authToken");
    }

    private static Client DecodeToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

        var client = new Client
        {
            Id = int.TryParse(jsonToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value, out int id) ? id : 0,
            FirstName = jsonToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value ?? string.Empty,
            Email = jsonToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value ?? string.Empty,
        };

        return client;
    }
}

public class LoginResponse
{
    public string? Token { get; set; }
}