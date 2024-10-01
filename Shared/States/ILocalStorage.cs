namespace GameStore.Shared.States;

public interface ILocalStorage
{
    Task<string?> GetToken(string storageKey);
    Task SetToken(string token, string storageKey);
    Task RemoveToken(string storageKey);
}