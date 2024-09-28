using GameStore.Dtos;
using GameStore.Models;
using GameStore.Repository;
using GameStore.Shared.Helpers;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;

namespace GameStore.Shared.States;

public class AuthState(LocalStorage localStorage) : AuthenticationStateProvider
{
    private readonly ClaimsPrincipal anonymous = new(new ClaimsIdentity());

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await localStorage.GetToken("auth-token");
        if (string.IsNullOrEmpty(token)) return await Task.FromResult(new AuthenticationState(anonymous));

        var client = AuthenticationService.DecodeToken(token);
        var getUserClaim = new CustomUserClaims(client.Id, client.FirstName, client.Email, "User");
        var claimsPrincipal = SetClaimPrincipal(getUserClaim);
        return await Task.FromResult(new AuthenticationState(claimsPrincipal));
    }

    public async Task UpdateAuthenticationState(string token)
    {
        var claimsPrincipal = new ClaimsPrincipal();
        if (!string.IsNullOrEmpty(token))
        {
            await localStorage.SetToken(token, "auth-token");
            var client = AuthenticationService.DecodeToken(token);
            string clientJson = JsonSerializer.Serialize(client);
            await localStorage.SetToken(clientJson, "client"); // checking purpose
            var getUserClaim = new CustomUserClaims(client.Id, client.FirstName, client.Email, "User");
            claimsPrincipal = SetClaimPrincipal(getUserClaim);
        }
        else
        {
            await localStorage.RemoveToken("auth-token");
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
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