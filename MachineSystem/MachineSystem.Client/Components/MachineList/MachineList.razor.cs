using MachineSystem.Domain.Entities;
using MachineSystem.Application.Services.MachineService;
using Microsoft.AspNetCore.Components;

namespace MachineSystem.Client.Components.MachineList;

public partial class MachineList
{
    private List<Machine>? machines;

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

    private async Task StartMachine(Guid machineId, MachineCommandState commandState)
    {
        commandState.Set(isPending: true);
        await MachineService.StartMachineAsync(machineId);
        commandState.Set(isPending: false);
        await FetchMachinesAsync();
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


