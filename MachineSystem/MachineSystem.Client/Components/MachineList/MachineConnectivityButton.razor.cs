using Microsoft.AspNetCore.Components;

namespace MachineSystem.BlazorClient.Components.MachineList;

public partial class MachineConnectivityButton
{
    [Parameter]
    public bool IsOnline { get; set; }

    [Parameter]
    public EventCallback OnClick { get; set; }
}
