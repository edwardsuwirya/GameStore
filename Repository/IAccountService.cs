using GameStore.Models;

namespace GameStore.Repository;

public interface IAccountService
{
    public Client? Login(UserAccess? userAccess);
}