using GameStore.Shared.Helpers;
using Microsoft.AspNetCore.Components;

namespace GameStore.Layout;

public partial class PageView : ComponentBase
{
    [Parameter] public RenderFragment Content { get; set; }
    public LoadingVals loadingVals;

    [Parameter] public string Title { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        loadingVals = new()
        {
            IsLoading = false,
            ValueChanged = IsLoadingChanged
        };
    }

    public void IsLoadingChanged()
    {
        StateHasChanged();
    }
}