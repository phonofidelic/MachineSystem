using System.ComponentModel.DataAnnotations;
using MachineSystem.Client.Pages;
using Microsoft.AspNetCore.Components;

namespace MachineSystem.Client.Components.MachineList;

public partial class MachineListTableRow
{
    [Parameter]
    public string Title { get; set; } = string.Empty;
    
    [Parameter]
    public string? SubTitle { get; set; }

    [Parameter]
    public  MachineStatus Status { get; set; }

    [Parameter]
    public string? LastData { get; set; }

    [Parameter]
    [DataType(DataType.Date)]
    public required DateTime LastUpdated { get; set; }

    [Parameter]
    public RenderFragment? Actions { get; set; }
}
