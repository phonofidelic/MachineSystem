
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

    private async Task StartMachine(Guid machineId)
    {
        await machineService.StartMachineAsync(machineId);
    }

    private async Task StopMachine(Guid machineId)
    {
        await machineService.StopMachineAsync(machineId);
    }

    private async Task ConnectMachine(Guid machineId)
    {
        await machineService.ConnectMachineAsync(machineId);
    }

    private async Task DisconnectMachine(Guid machineId)
    {
        await machineService.DisconnectMachineAsync(machineId);
    }
}

