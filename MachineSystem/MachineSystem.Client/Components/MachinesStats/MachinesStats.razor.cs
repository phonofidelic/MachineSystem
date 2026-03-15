using MachineSystem.Application.ViewModels;
using Microsoft.AspNetCore.Components;

namespace MachineSystem.BlazorClient.Components.MachinesStats;

public partial class MachinesStats
{
    private bool IsOpen = false;

    [Parameter]
    public IReadOnlyList<MachineListItem>? Machines { get; set; } = null;

    private int MachinesTotalCount { get; set; }

    private int MachinesOnlineCount { get; set; }
    private decimal MachinesOnlinePercentage { get; set; }

    private int MachinesOperationalCount { get; set; }
    private decimal MachinesOperationalPercentage { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (Machines is null || Machines.Count < 1) return;

        MachinesTotalCount = Machines.Count;

        MachinesOnlineCount = Machines.Count(m => m.Status.IsOnline);

        MachinesOnlinePercentage = decimal.Round((decimal)MachinesOnlineCount / MachinesTotalCount * 100, 2);

        MachinesOperationalCount = Machines.Count(m => m.Status.IsOperational);

        MachinesOperationalPercentage = decimal.Round((decimal)MachinesOperationalCount / MachinesTotalCount * 100, 2);
    }   

    private void ToggleOpen() {
        IsOpen = !IsOpen;
    }
}
