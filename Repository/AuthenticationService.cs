using GameStore.Dtos;
using GameStore.Models;
using GameStore.Shared.Helpers;
using GameStore.Shared.Services;
using GameStore.Shared.States;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GameStore.Repository;

public class AuthenticationService(IHttpService httpService, LocalStorage localStorage) : IAuthenticationService
{
    public async Task<ResponseWrapper<LoginResponse>> Login(UserAccess userAccess)
    {
        try
        {
            var response = await httpService.Post<LoginResponse>("/api/users/authenticate", userAccess);
            if (!string.IsNullOrEmpty(response.Token))
            {
                await localStorage.SetToken(response.Token, "auth-token");
                return ResponseWrapper<LoginResponse>.Success(response);
            }
            return ResponseWrapper<LoginResponse>.Fail("Invalid response from server");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Access denied. Exception: {e.Message}");
            return ResponseWrapper<LoginResponse>.Fail("Access denied");
        }
    }

    public static Client DecodeToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

        var client = new Client
        {
            Id = int.TryParse(jsonToken.Claims.FirstOrDefault(claim => claim.Type == "unique_name")?.Value, out var id) ? id : 0,
            FirstName = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "given_name")?.Value ?? string.Empty,
            LastName = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "family_name")?.Value ?? string.Empty,
            Email = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "email")?.Value ?? string.Empty,
            Address = jsonToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.StreetAddress)?.Value ?? string.Empty,
            Phone = jsonToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.MobilePhone)?.Value ?? string.Empty
        };

        return client;
    }
}