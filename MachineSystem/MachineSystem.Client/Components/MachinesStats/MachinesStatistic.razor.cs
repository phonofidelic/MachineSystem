using System;
using Microsoft.AspNetCore.Components;

namespace MachineSystem.BlazorClient.Components.MachinesStats;

public partial class MachinesStatistic
{
    [Parameter]
    public bool IsLoading { get; set; } = true;

    [Parameter]
    public string Title { get; set; } = string.Empty;

    [Parameter]
    public string Metric { get; set; } = string.Empty;

    [Parameter]
    public string Description { get; set; } = string.Empty;
}
