
using MachineSystem.Application.Queries;
using MachineSystem.Application.ServiceContracts;
using MachineSystem.Application.ViewModels;
using Microsoft.AspNetCore.Components;

namespace MachineSystem.BlazorClient.Pages;
public partial class Home
{
    private IReadOnlyList<MachineListItem>? machines = [];

    

    private string? ErrorMessage { get; set; } = null;

    protected override async Task OnInitializedAsync()
    {
        await FetchMachinesAsync();
    }

    [Inject]
    private IMachineApiClient MachineApiClient { get; set; } = default!;

    // ToDo: Cache result of first fetch?
    private async Task FetchMachinesAsync()
    {
        var result = await MachineApiClient.GetMachinesAsync(new GetMachinesQuery());

        if (result == null) {
            ErrorMessage = "Could not fetch Machines";
            await Task.Delay(3000);
            ErrorMessage = null;
            return;
        }

        machines = result.Machines
            .OrderByDescending(m => m.LastUpdated)
            .ToList();
    }

    private async Task UpdateMachinesListAsync(IReadOnlyList<MachineListItem> updatedMachinesList)
    {
        machines = updatedMachinesList;
    }

    private async Task UpdateMachinesListWithUpdaterAsync(Func<IReadOnlyList<MachineListItem>, IReadOnlyList<MachineListItem>> updateMachinesList)
    {
        machines = updateMachinesList(machines ?? []);
    }
};