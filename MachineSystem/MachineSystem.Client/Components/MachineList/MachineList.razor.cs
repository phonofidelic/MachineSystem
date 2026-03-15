using Microsoft.AspNetCore.Components;
using MachineSystem.Application.ServiceContracts;
using MachineSystem.Application.Commands;
using MachineSystem.Application.ViewModels;
using MachineSystem.Domain.ValueObjects;

namespace MachineSystem.BlazorClient.Components.MachineList;

public partial class MachineList
{
    [Parameter]
    public IReadOnlyList<MachineListItem>? Machines { get; set; } = null;

    [Parameter]
    public EventCallback<IReadOnlyList<MachineListItem>> OnMachinesListUpdated { get; set; }

    private string? ErrorMessage { get; set; } = null;

    private ErrorBoundaryBase? errorBoundary;

    [Inject]
    private IMachineApiClient MachineApiClient { get; set; } = default!;

    private async Task UpdateMachineStatus(Guid machineId, MachineActionResult result)
    {
        Machines = Machines?.Select(m =>
        {
            if (m.Id != machineId) return m;
            m.Status = new MachineStatus(
                isOnline: result.IsOnline,
                isOperational: result.IsOperational,
                isRunning: result.IsRunning
            );
            return m;
        }).ToList();

        if (OnMachinesListUpdated.HasDelegate)
            await OnMachinesListUpdated.InvokeAsync(Machines);

        StateHasChanged();
    }
    
    private async Task StartMachine(Guid machineId, MachineCommandState commandState)
    {
        //try
        //{
            commandState.Set(isPending: true);

            var result = await MachineApiClient.StartMachineAsync(new StartMachineCommand(machineId));
            await UpdateMachineStatus(machineId, result);

            commandState.Set(isPending: false);
        //} catch(Exception ex)
        //{
        //    commandState.Set(isPending: false);
        //    // Show error UI
        //    ErrorMessage = GetUiErrorMessage(ex);
        //    await Task.Delay(3000);
        //    ErrorMessage = null;
        //}
    }

    private async Task StopMachine(Guid machineId, MachineCommandState commandState)
    {
        commandState.Set(isPending: true);

        var result = await MachineApiClient.StopMachineAsync(new StopMachineCommand(machineId));
        await UpdateMachineStatus(machineId, result);

        commandState.Set(isPending: false);
    }

    private async Task ConnectMachine(Guid machineId, MachineCommandState? commandState = null)
    {
        commandState?.Set(isPending: true);

        var result = await MachineApiClient.ConnectMachineAsync(new ConnectMachineCommand(machineId));
        await UpdateMachineStatus(machineId, result);

        commandState?.Set(isPending: false);
    }

    private async Task DisconnectMachine(Guid machineId, MachineCommandState? commandState = null)
    {
        commandState?.Set(isPending: true);

        var result = await MachineApiClient.DisconnectMachineAsync(new DisconnectMachineCommand(machineId));
        await UpdateMachineStatus(machineId, result);

        commandState?.Set(isPending: false);
    }
}


