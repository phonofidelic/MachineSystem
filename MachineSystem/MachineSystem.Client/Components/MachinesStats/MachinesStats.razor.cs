using MachineSystem.Application.ViewModels;
using Microsoft.AspNetCore.Components;

namespace MachineSystem.BlazorClient.Components.MachinesStats;

public partial class MachinesStats
{
    private bool IsOpen = false;

    [Parameter]
    public IReadOnlyList<MachineListItem>? Machines { get; set; } = null;

    private PercentageMetric MachinesOnlineMetric { get; set; } = new (0, 0);
    
    private PercentageMetric MachinesOperationalMetric { get; set; } = new (0, 0);

    private PercentageMetric MachinesRunningMetric { get; set; } = new(0, 0);

    protected override async Task OnParametersSetAsync()
    {
        if (Machines is null || Machines.Count < 1) return;

        MachinesOnlineMetric = new PercentageMetric(Machines.Count, Machines.Count(m => m.Status.IsOnline));
        MachinesOperationalMetric = new PercentageMetric(Machines.Count(m => m.Status.IsOperational), Machines.Count);
        MachinesRunningMetric = new PercentageMetric(Machines.Count(m => m.Status.IsRunning), Machines.Count);
    }

    private void ToggleOpen() {
        IsOpen = !IsOpen;
    }
}

