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
            Id = int.TryParse(jsonToken?.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value, out var id) ? id : 0,
            FirstName = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value ?? string.Empty,
            LastName = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "LastName")?.Value ?? string.Empty,
            Email = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value ?? string.Empty,
            Address = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "Address")?.Value ?? string.Empty,
            Phone = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "Phone")?.Value ?? string.Empty
        };

        return client;
    }
}