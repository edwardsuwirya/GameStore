using Microsoft.AspNetCore.Components;

namespace GameStore.Pages.Game;

public partial class DeleteGame : ComponentBase
{
    [Parameter] public Models.Game? Game { get; set; }

    private string title = string.Empty;

    [Parameter] public bool IsClosing { get; set; }
    [Parameter] public EventCallback<bool> IsClosingChanged { get; set; }

    protected override void OnParametersSet()
    {
        title = $"Delete {Game?.Name}?";
    }

    private void Confirm()
    {
        IsClosingChanged.InvokeAsync(true);
    }

    private void Cancel()
    {
        IsClosingChanged.InvokeAsync(false);
    }
}