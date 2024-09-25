using GameStore.Shared.Navigation;
using Microsoft.AspNetCore.Components;

namespace GameStore.Pages.Game;

public partial class EditGame : ComponentBase
{
    [Parameter] public int? Id { get; set; }
    private bool isLoading;
    private bool isSubmitting;

    protected override async Task OnParametersSetAsync()
    {
        isLoading = true;
        if (Id is not null)
        {
            var foundGame = await GameService.GetGameById(Id.Value);
            if (foundGame.IsSuccess)
            {
                game = new Models.Game
                {
                    Id = foundGame.Data.Id,
                    Name = foundGame.Data.Name,
                    Genre = foundGame.Data.Genre,
                    Price = foundGame.Data.Price,
                    ReleaseDate = foundGame.Data.ReleaseDate
                };
                title = $"Edit {game.Name}";
            }
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

        isLoading = false;
    }

    private Models.Game? game;
    private string title = string.Empty;

    private async void HandleSubmit()
    {
        isSubmitting = true;
        if (game!.Id == 0)
        {
            await GameService.AddGame(game);
        }
        else
        {
            await GameService.UpdateGame(game);
        }

        NavManager.NavigateTo(PageRoute.Game);
    }

    private void Cancel()
    {
        NavManager.NavigateTo(PageRoute.Game);
    }
}