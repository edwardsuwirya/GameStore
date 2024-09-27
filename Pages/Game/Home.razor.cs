using GameStore.Shared.Navigation;
using Microsoft.AspNetCore.Components;

namespace GameStore.Pages.Game;

public partial class Home : ComponentBase
{
    private Models.Game[]? games;
    private Models.Game? currentGame;
    private bool _isModalClosing;

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
        LoadingState.SetOnLoading(true);
        await GameService.DeleteGame(id);
        GetGames();
    }

    private async void GetGames()
    {
        LoadingState.SetOnLoading(true);
        var gameList = await GameService.GetGames();
        if (gameList.IsSuccess)
        {
            LoadingState.SetOnLoading(false);
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