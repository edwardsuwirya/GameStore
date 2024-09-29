using GameStore.Models;
using GameStore.Shared.Helpers;
using GameStore.Shared.Services;
using GameStore.Shared.Responses;

namespace GameStore.Repository;

public class AuthenticationService(IHttpService httpService) : IAuthenticationService
{
    public async Task<ResponseWrapper<LoginResponse>> Login(UserAccess userAccess)
    {
        try
        {
            var response = await httpService.Post<LoginResponse>("/api/users/authenticate", userAccess);
            if (!string.IsNullOrEmpty(response.Token))
            {
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
}