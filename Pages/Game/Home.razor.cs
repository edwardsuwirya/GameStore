using GameStore.Shared.Errors;
using GameStore.Shared.Navigation;
using GameStore.Shared.Responses;
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
        var result = await DeleteGameService.Execute(id);
        result.Match(onSuccess: OnSuccessDeleteGame, onFailure: OnFailure);
    }

    private async void GetGames()
    {
        LoadingState.SetOnLoading(true);
        var gameList = await GameGameListService.Execute();
        gameList.Match(onSuccess: OnSuccessGetGames, onFailure: OnFailure);
    }

    private void OnFailure(AppError errorMessage)
    {
        if (errorMessage.Code == ErrorCode.GeneralError || errorMessage.Code == ErrorCode.PathNotFound)
        {
            NavManager.NavigateTo(PageRoute.Error, forceLoad: true);
        }
        else
        {
            LoadingState.SetOnLoading(false);
            StateHasChanged();
        }
    }

    private void OnSuccessDeleteGame(int id)
    {
        GetGames();
    }

    private void OnSuccessGetGames(Models.Game[] listGame)
    {
        LoadingState.SetOnLoading(false);
        games = listGame;
        StateHasChanged();
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