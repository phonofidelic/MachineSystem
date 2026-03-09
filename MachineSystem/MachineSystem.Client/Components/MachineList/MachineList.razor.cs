
using MachineSystem.Client.Services;
using MachineSystem.Domain.Entities;
using MachineSystem.Domain.Services;

namespace MachineSystem.Client.Components.MachineList;

public partial class MachineList
{
    private readonly IMachineService machineService = new MockClientMachineService();
    
    private List<Machine>? machines;

    protected override async Task OnInitializedAsync()
    {
        machines = await machineService.GetMachinesAsync();
    }

    private async Task StartMachine(Guid machineId, MachineCommandState commandState)
    {
        commandState.Set(isPending: true);
        await machineService.StartMachineAsync(machineId);
        commandState.Set(isPending: false);
    }

    private async Task StopMachine(Guid machineId, MachineCommandState commandState)
    {
        commandState.Set(isPending: true);
        await machineService.StopMachineAsync(machineId);
        commandState.Set(isPending: false);
    }

    private async Task ConnectMachine(Guid machineId, MachineCommandState? commandState = null)
    {
        commandState?.Set(isPending: true);
        await machineService.ConnectMachineAsync(machineId);
        commandState?.Set(isPending: false);
    }

    private async Task DisconnectMachine(Guid machineId, MachineCommandState? commandState = null)
    {
        commandState?.Set(isPending: true);
        await machineService.DisconnectMachineAsync(machineId);
        commandState?.Set(isPending: false);
    }
}


