using GameStore.Layout;
using GameStore.Shared.Helpers;
using GameStore.Shared.Navigation;
using Microsoft.AspNetCore.Components;

namespace GameStore.Pages.Game;

public partial class Home : ComponentBase
{
    private Models.Game[]? games;
    private Models.Game? currentGame;
    private bool _isModalClosing;
    private bool isLoading;

    [CascadingParameter] public LoadingVals LoadingValsAncestor { get; set; } = new();
    [CascadingParameter] public PageView? Ancestor { get; set; }

    private bool IsModalClosing
    {
        get => _isModalClosing;
        set
        {
            _isModalClosing = value;
            if (!_isModalClosing) return;
            DeleteGame(currentGame!.Id);
        }
    }

    protected override void OnInitialized()
    {
        GetGames();
    }


    private async void DeleteGame(int id)
    {
        isLoading = true;
        await GameService.DeleteGame(id);
        GetGames();
        StateHasChanged();
    }

    private async void GetGames()
    {
        isLoading = true;
        var gameList = await GameService.GetGames();
        if (gameList.IsSuccess)
        {
            isLoading = false;
            games = gameList.Data;
            StateHasChanged();
        }
    }

    private void CreateGame()
    {
        NavManager.NavigateTo(PageRoute.NewGame);
    }

    private void EditGame(int id)
    {
        NavManager.NavigateTo($"{PageRoute.Game}/{id}");
    }
}