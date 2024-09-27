using Microsoft.AspNetCore.Components;

namespace GameStore.Layout;

public partial class PageView : ComponentBase, IDisposable
{
    [Parameter] public RenderFragment ChildContent { get; set; }

    [Parameter] public string Title { get; set; }

    protected override void OnInitialized()
    {
        LoadingState.OnChange += StateHasChanged;
    }

    public void Dispose()
    {
        LoadingState.OnChange -= StateHasChanged;
    }
}