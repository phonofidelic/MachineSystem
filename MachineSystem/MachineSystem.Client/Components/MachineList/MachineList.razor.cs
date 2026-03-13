using Microsoft.AspNetCore.Components;
using MachineSystem.Application.Services.MachineService.Exceptions;
using MachineSystem.Application.ServiceContracts;
using MachineSystem.Application.Queries;
using MachineSystem.Application.Commands;
using MachineSystem.Application.ViewModels;
using MachineSystem.Domain.ValueObjects;

namespace MachineSystem.BlazorClient.Components.MachineList;

public partial class MachineList
{
    private IReadOnlyList<MachineListItem>? machines = [];

    private string? ErrorMessage { get; set; } = null;

    private ErrorBoundaryBase? errorBoundary;

    [Inject]
    private IMachineApiClient MachineApiClient { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await FetchMachinesAsync();
    }

    // ToDo: Cache result of first fetch?
    private async Task FetchMachinesAsync()
    {
        var result = await MachineApiClient.GetMachinesAsync(new GetMachinesQuery());

        if (result == null) {
            ErrorMessage = GetUiErrorMessage(new MachineNotFoundException());
            await Task.Delay(3000);
            ErrorMessage = null;
            return;
        }

        machines = result.Machines.ToList();
    }

    private static string GetUiErrorMessage(Exception ex)
    {
        return ex is MachineNotFoundException
            ? "This Machine could not be found"
            : "An unexpected error occurred";
    }

    private void UpdateMachineStatus(Guid machineId, MachineActionResult result)
    {
        machines = machines?.Select(m =>
        {
            if (m.Id != machineId) return m;
            m.Status = new MachineStatus(
                isOnline: result.IsOnline,
                isOperational: result.IsOperational,
                isRunning: result.IsRunning
            );
            return m;
        }).ToList();
    }
    
    private async Task StartMachine(Guid machineId, MachineCommandState commandState)
    {
        //try
        //{
            commandState.Set(isPending: true);

            var result = await MachineApiClient.StartMachineAsync(new StartMachineCommand(machineId));
            UpdateMachineStatus(machineId, result);

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
        UpdateMachineStatus(machineId, result);

        commandState.Set(isPending: false);
    }

    private async Task ConnectMachine(Guid machineId, MachineCommandState? commandState = null)
    {
        commandState?.Set(isPending: true);

        var result = await MachineApiClient.ConnectMachineAsync(new ConnectMachineCommand(machineId));
        UpdateMachineStatus(machineId, result);

        commandState?.Set(isPending: false);
    }

    private async Task DisconnectMachine(Guid machineId, MachineCommandState? commandState = null)
    {
        commandState?.Set(isPending: true);

        var result = await MachineApiClient.DisconnectMachineAsync(new DisconnectMachineCommand(machineId));
        UpdateMachineStatus(machineId, result);

        commandState?.Set(isPending: false);
    }
}


