using Microsoft.AspNetCore.Components;

namespace GameStore.Shared.Components;

public partial class CommonGrid<TItem> : ComponentBase
{
    [Parameter] public RenderFragment Header { get; set; }
    [Parameter] public RenderFragment ActionHeader { get; set; }
    [Parameter] public RenderFragment<TItem> Row { get; set; }
    [Parameter] public RenderFragment Footer { get; set; }
    [Parameter] public TItem[]? Items { get; set; }
}