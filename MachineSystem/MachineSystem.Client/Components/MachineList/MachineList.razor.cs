using MachineSystem.Domain.Entities;
using MachineSystem.Application.Services.MachineService;
using Microsoft.AspNetCore.Components;
using MachineSystem.Application.Services.MachineService.Exceptions;
using MachineSystem.Application.Services.MachineService.Dtos;

namespace MachineSystem.Client.Components.MachineList;

public partial class MachineList
{
    // ToDo: Make read-only
    // This list should only be modified through the API, not direct access.
    // Should it be modifiable through the Machine entity's API?
    private IReadOnlyList<Machine>? machines;

    private string? errorMessage { get; set; } = null;

    [Inject]
    private IMachineService MachineService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await FetchMachinesAsync();
    }

    // ToDo: Should not need to re-fetch machines list after each state update?
    private async Task FetchMachinesAsync()
    {
        machines = await MachineService.GetMachinesAsync();
    }

    private string GetUiErrorMessage(Exception ex)
    {
        return ex is MachineNotFoundException
            ? "This Machine could not be found"
            : "An unexpected error occurred";
    }
    private async Task StartMachine(Guid machineId, MachineCommandState commandState)
    {
        try
        {
            var machineToUpdate = machines?.FirstOrDefault(m => m.Id == machineId) ?? throw new MachineNotFoundException();

            commandState.Set(isPending: true);

            var result = await MachineService.StartMachineAsync(new StartMachineCommandDto(machineId));

            machineToUpdate.SetStatus(new(
                isOnline: result.IsOnline,
                isOperational: result.IsOperational,
                isRunning: result.IsRunning));

            commandState.Set(isPending: false);
        } catch(Exception ex)
        {
            commandState.Set(isPending: false, isError: true);
            // Show error UI
            errorMessage = GetUiErrorMessage(ex);
            await Task.Delay(3000);
            errorMessage = null;
        }
    }

    private async Task StopMachine(Guid machineId, MachineCommandState commandState)
    {
        commandState.Set(isPending: true);
        await MachineService.StopMachineAsync(machineId);
        commandState.Set(isPending: false);
        await FetchMachinesAsync();
    }

    private async Task ConnectMachine(Guid machineId, MachineCommandState? commandState = null)
    {
        commandState?.Set(isPending: true);
        await MachineService.ConnectMachineAsync(machineId);
        commandState?.Set(isPending: false);
        await FetchMachinesAsync();
    }

    private async Task DisconnectMachine(Guid machineId, MachineCommandState? commandState = null)
    {
        commandState?.Set(isPending: true);
        await MachineService.DisconnectMachineAsync(machineId);
        commandState?.Set(isPending: false);
        await FetchMachinesAsync();
    }
}


