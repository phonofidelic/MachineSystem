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
    // ToDo: Make read-only
    // This list should only be modified through the API, not direct access.
    // Should it be modifiable through the Machine entity's API?
    private List<MachineListItem>? machines = [];

    private string? errorMessage { get; set; } = null;

    [Inject]
    private IMachineApiClient MachineApiClient { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await FetchMachinesAsync();
    }

    // ToDo: Should not need to re-fetch machines list after each state update?
    private async Task FetchMachinesAsync()
    {
        var result = await MachineApiClient.GetMachinesAsync(new GetMachinesQuery());

        if (result == null) {
            errorMessage = GetUiErrorMessage(new MachineNotFoundException());
            await Task.Delay(3000);
            errorMessage = null;
            return;
        }

        machines = result.Machines.ToList();
    }

    private string GetUiErrorMessage(Exception ex)
    {
        return ex is MachineNotFoundException
            ? "This Machine could not be found"
            : "An unexpected error occurred";
    }

    // [Parameter]
    // public EventCallback<MachineCommandButton> OnCommandClicked { get; set; }
    
    private async Task StartMachine(Guid machineId, MachineCommandState commandState)
    {
        Console.WriteLine("START MACHINE CLICKED");
        try
        {
            //var machineToUpdate = machines?.FirstOrDefault(m => m.Id == machineId) ?? throw new MachineNotFoundException();

            commandState.Set(isPending: true);

            var result = await MachineApiClient.StartMachineAsync(new StartMachineCommand(machineId));

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
        var result = await MachineApiClient.StopMachineAsync(new StopMachineCommand(machineId));
        commandState.Set(isPending: false);
        await FetchMachinesAsync();
    }

    private async Task ConnectMachine(Guid machineId, MachineCommandState? commandState = null)
    {
        commandState?.Set(isPending: true);
        var result = await MachineApiClient.ConnectMachineAsync(new ConnectMachineCommand(machineId));
        commandState?.Set(isPending: false);
        await FetchMachinesAsync();
    }

    private async Task DisconnectMachine(Guid machineId, MachineCommandState? commandState = null)
    {
        commandState?.Set(isPending: true);
        var result = await MachineApiClient.DisconnectMachineAsync(new DisconnectMachineCommand(machineId));
        commandState?.Set(isPending: false);
        await FetchMachinesAsync();
    }
}


