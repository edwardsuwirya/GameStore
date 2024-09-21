using GameStore.Models;
using GameStore.Shared.States;

namespace GameStore.Repository;

public class AccountService(AuthState authState) : IAccountService
{
    public Client? Login(UserAccess? userAccess)
    {
        if (userAccess != null &&
            (!userAccess.UserName.Equals("edo") || !userAccess.Password.Equals("123456"))) return null;
        var client = new Client
        {
            Id = 1,
            FirstName = "Edo",
            LastName = "",
            Address = "123 Main Street",
            Email = "edo@gmail.com",
            Phone = "123456"
        };
        authState.Client = client;

        return null;
    }
}