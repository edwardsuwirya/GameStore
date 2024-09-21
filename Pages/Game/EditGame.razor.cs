using Microsoft.AspNetCore.Components;

namespace GameStore.Pages.Game;

public partial class EditGame : ComponentBase
{
    [Parameter] public int? Id { get; set; }

    protected override void OnParametersSet()
    {
        if (Id is not null)
        {
            var foundGame = GameService.GetGameById(Id.Value);
            game = new Models.Game
            {
                Id = foundGame.Id,
                Name = foundGame.Name,
                Genre = foundGame.Genre,
                Price = foundGame.Price,
                ReleaseDate = foundGame.ReleaseDate
            };
            title = $"Edit {game.Name}";
        }
        else
        {
            game = new Models.Game
            {
                Name = string.Empty,
                Genre = string.Empty,
                ReleaseDate = DateTime.UtcNow
            };
            title = "New Game";
        }
    }

    private Models.Game? game;
    private string title = string.Empty;

    private void HandleSubmit()
    {
        if (game!.Id == 0)
        {
            GameService.AddGame(game);
        }
        else
        {
            GameService.UpdateGame(game);
        }

        NavManager.NavigateTo("/Game");
    }

    private void Cancel()
    {
        NavManager.NavigateTo("/Game");
    }
}