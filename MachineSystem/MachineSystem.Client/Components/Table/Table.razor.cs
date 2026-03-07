using Microsoft.AspNetCore.Components;

namespace MachineSystem.Client.Components.Table;

public partial class Table
{
    [Parameter]
    public required RenderFragment TableHeader { get; set; }

    [Parameter]
    public required RenderFragment TableBody { get; set; }
}
