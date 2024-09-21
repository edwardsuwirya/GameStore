using Microsoft.AspNetCore.Components;

namespace GameStore.Shared.Components;

public partial class GenreSelection : ComponentBase
{
    [Parameter] public string Id { get; set; } = "genre";

    private string selectedGenre { get; set; }

    [Parameter]
    public string SelectedGenre
    {
        get => selectedGenre;
        set
        {
            if (value == selectedGenre)
                return;

            selectedGenre = value;
            if (SelectedGenreChanged.HasDelegate)
            {
                SelectedGenreChanged.InvokeAsync(selectedGenre);
            }
        }
    }

    [Parameter] public EventCallback<string> SelectedGenreChanged { get; set; }
}