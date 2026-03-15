using Microsoft.AspNetCore.Components;

namespace MachineSystem.BlazorClient.Components.MachinesStats;

public partial class MachinesStatistic
{
    [Parameter]
    public required RenderFragment Metric { get; set; }
}