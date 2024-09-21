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
            GameService.DeleteGame(currentGame!.Id);
            games = GameService.GetGames();
        }
    }

    protected override void OnInitialized()
    {
        games = GameService.GetGames();
    }

    private void CreateGame()
    {
        NavManager.NavigateTo("/Game/New");
    }

    private void EditGame(int id)
    {
        NavManager.NavigateTo($"/Game/{id}");
    }
}