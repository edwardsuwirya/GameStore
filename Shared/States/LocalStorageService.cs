using Blazored.LocalStorage;

namespace GameStore.Shared.States;

public class LocalStorageService(ILocalStorageService localStorageService)
{
    public async Task<string?> GetToken(string storageKey)
    {
        return await localStorageService.GetItemAsStringAsync(storageKey);
    }

    public async Task SetToken(string token, string storageKey) =>
        await localStorageService.SetItemAsStringAsync(storageKey, token);

    public async Task RemoveToken(string storageKey) => await localStorageService.RemoveItemAsync(storageKey);
}