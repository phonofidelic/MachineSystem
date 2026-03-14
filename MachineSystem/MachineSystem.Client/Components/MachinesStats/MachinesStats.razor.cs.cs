using MachineSystem.Application.ViewModels;
using Microsoft.AspNetCore.Components;

namespace MachineSystem.BlazorClient.Components.MachinesStats;

public partial class MachinesStats
{
    [Parameter]
    public IReadOnlyList<MachineListItem>? Machines { get; set; } = null;
}
