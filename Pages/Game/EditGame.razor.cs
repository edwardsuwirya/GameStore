using GameStore.Shared.Errors;
using GameStore.Shared.Navigation;
using GameStore.Shared.Responses;
using Microsoft.AspNetCore.Components;

namespace GameStore.Pages.Game;

public partial class EditGame : ComponentBase
{
    [Parameter] public int? Id { get; set; }
    private bool isSubmitting;
    private Models.Game? game;
    private string title = string.Empty;

    protected override async Task OnParametersSetAsync()
    {
        LoadingState.SetOnLoading(true);
        if (Id is not null)
        {
            var foundGameResult = await GetGameService.Execute(Id.Value);
            foundGameResult.Match(onSuccess: (foundGame) =>
            {
                game = new Models.Game
                {
                    Id = foundGame.Id,
                    Name = foundGame.Name,
                    Genre = foundGame.Genre,
                    Price = foundGame.Price,
                    ReleaseDate = foundGame.ReleaseDate
                };
                title = $"Edit {game.Name}";
                LoadingState.SetOnLoading(false);
                StateHasChanged();
            }, onFailure: (_) => NavManager.NavigateTo(PageRoute.Error, forceLoad: true));
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
            LoadingState.SetOnLoading(false);
            StateHasChanged();
        }
    }

    private async void HandleSubmit()
    {
        isSubmitting = true;
        ResponseWrapper<Models.Game> result;
        if (game!.Id == 0)
        {
            result = await AddGameService.Execute(game);
        }
        else
        {
            result = await UpdateGameService.Execute(game);
        }

        result.Match(onSuccess: OnSuccess, onFailure: OnFailure);
    }

    private void OnSuccess(Models.Game game)
    {
        NavManager.NavigateTo(PageRoute.Game);
    }

    private void OnFailure(AppError errorMessage)
    {
        if (errorMessage.Code is ErrorCode.GeneralError or ErrorCode.PathNotFound)
        {
            NavManager.NavigateTo(PageRoute.Error, forceLoad: true);
        }
        else
        {
            isSubmitting = false;
            StateHasChanged();
        }
    }

    private void Cancel()
    {
        NavManager.NavigateTo(PageRoute.Game);
    }
}