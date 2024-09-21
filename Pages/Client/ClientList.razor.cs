using Microsoft.AspNetCore.Components;

namespace GameStore.Pages.Client;

public partial class ClientList : ComponentBase
{
    [Parameter] public RenderFragment NullClientList { get; set; }
    [Parameter] public RenderFragment EmptyClientList { get; set; }

    private Models.Client[]? Clients;
    private Models.Client? CurrentClient;

    protected override async Task OnInitializedAsync()
    {
        await Task.Delay(2000);
        Clients = ClientService.GetClient();
    }

    private void CreateClient()
    {
        NavManager.NavigateTo("/Client/New");
    }

    private void EditClient(int id)
    {
        NavManager.NavigateTo($"/Client/{id}");
    }
}