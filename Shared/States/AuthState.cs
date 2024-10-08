using System.Security.Claims;
using GameStore.Dtos;
using GameStore.Models;
using GameStore.Shared.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;

namespace GameStore.Shared.States;

public class AuthState(LocalStorage localStorage) : AuthenticationStateProvider
{
    private readonly ClaimsPrincipal anonymous = new(new ClaimsIdentity());

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var stringToken = await localStorage.GetToken("auth-token");
        if (string.IsNullOrEmpty(stringToken)) return await Task.FromResult(new AuthenticationState(anonymous));

        var clientToken = JsonConvert.DeserializeObject<Client>(stringToken);
        if (clientToken == null) return await Task.FromResult(new AuthenticationState(anonymous));

        var getUserClaim = new CustomUserClaims(clientToken.Id, clientToken.FirstName, clientToken.Email, "User");
        var claimsPrincipal = SetClaimPrincipal(getUserClaim);
        return await Task.FromResult(new AuthenticationState(claimsPrincipal));
    }

    public async Task UpdateAuthenticationState(Client? client)
    {
        var claimPrincipal = new ClaimsPrincipal();
        if (client != null)
        {
            var serializedToken = JsonConvert.SerializeObject(client);
            await localStorage.SetToken(serializedToken, "auth-token");
            var getUserClaim = new CustomUserClaims(client.Id, client.FirstName, client.Email, "User");
            claimPrincipal = SetClaimPrincipal(getUserClaim);
        }
        else
        {
            await localStorage.RemoveToken("auth-token");
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimPrincipal)));
    }

    private static ClaimsPrincipal SetClaimPrincipal(CustomUserClaims claims)
    {
        if (claims.Email == null) return new ClaimsPrincipal();
        return new ClaimsPrincipal(new ClaimsIdentity(
            new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, claims.Id.ToString()),
                new(ClaimTypes.Name, claims.Name),
                new(ClaimTypes.Email, claims.Email),
                new(ClaimTypes.Role, claims.Role),
            }, "JwtAuth"));
    }
}