using Microsoft.AspNetCore.Components;

namespace MachineSystem.BlazorClient.Components.MachineList;

public partial class MachineCommandButton
{
    [Parameter]
    public MachineCommandState CommandState { get; set; } = new();

    [Parameter]
    public RenderFragment? ButtonContent { get; set; }

    [Parameter]
    public bool Disabled { get; set; } = false;

    [Parameter]
    public EventCallback<MachineCommandState> OnClick { get; set; }
    // public EventCallback<MachineCommandButton> OnClick { get; set; }

    [Parameter]
    public string ClassName { get; set; } = string.Empty;

    private async Task InvokeCommandAsync()
    {
        Console.WriteLine($"CLICK InvokeCommandAsync, HasDelegate: {OnClick.HasDelegate}");
        if (OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync(CommandState);
        }
    }
}
