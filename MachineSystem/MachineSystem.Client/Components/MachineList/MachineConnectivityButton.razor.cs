using Microsoft.AspNetCore.Components;

namespace MachineSystem.Client.Components.MachineList;

public partial class MachineConnectivityButton
{
    [Parameter]
    public bool IsOnline { get; set; }

    [Parameter]
    public EventCallback OnClick { get; set; }
}
