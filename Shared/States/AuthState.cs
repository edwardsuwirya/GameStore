using GameStore.Dtos;
using GameStore.Models;
using GameStore.Shared.Helpers;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Blazored.LocalStorage;

namespace GameStore.Shared.States;

public class AuthState(ILocalStorage localStorage) : AuthenticationStateProvider
{
    private readonly ClaimsPrincipal anonymous = new(new ClaimsIdentity());

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await localStorage.GetToken("auth-token");
        if (string.IsNullOrEmpty(token)) return await Task.FromResult(new AuthenticationState(anonymous));

        var client = DecodeToken(token);
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
            var client = DecodeToken(token);
            string clientJson = JsonSerializer.Serialize(client);
            await localStorage.SetToken(clientJson, "client"); // checking purpose
            var getUserClaim = new CustomUserClaims(client.Id, client.FirstName, client.Email, "User");
            claimsPrincipal = SetClaimPrincipal(getUserClaim);
        }
        else
        {
            await localStorage.RemoveToken("auth-token");
            await localStorage.RemoveToken("client");
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

    public static Client DecodeToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

        var client = new Client
        {
            Id = int.TryParse(jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "nameid")?.Value, out var id) ? id : 0,
            FirstName = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "given_name")?.Value ?? string.Empty,
            LastName = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "family_name")?.Value ?? string.Empty,
            Email = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "email")?.Value ?? string.Empty,
            Address = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.StreetAddress)?.Value ?? string.Empty,
            Phone = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.MobilePhone)?.Value ?? string.Empty
        };

        return client;
    }
}