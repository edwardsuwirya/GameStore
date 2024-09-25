using GameStore.Models;
using GameStore.Shared.Helpers;

namespace GameStore.Repository;

public class AuthenticationService : IAuthenticationService
{

    public async Task<ResponseWrapper<Client>> Login(UserAccess userAccess)
    {
        await Task.Delay(1000).ConfigureAwait(false);
        if (!userAccess.UserName.Equals("edo") || !userAccess.Password.Equals("123456"))
            return ResponseWrapper<Client>.Fail("Access denied");

        var user = new Client
        {
            Id = 1,
            FirstName = "Edo",
            LastName = "",
            Address = "123 Main Street",
            Email = "edo@gmail.com",
            Phone = "123456"
        };
        return ResponseWrapper<Client>.Success(user);
    }
}