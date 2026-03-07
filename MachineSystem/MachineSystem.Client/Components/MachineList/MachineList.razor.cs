
using MachineSystem.Client.Services;
using MachineSystem.Domain.Entities;
using MachineSystem.Domain.Services;

namespace MachineSystem.Client.Components.MachineList;

public partial class MachineList
{
    private readonly IMachineService machineService = new MockMachineService();
    private List<Machine> machines = [];

    protected async override Task OnInitializedAsync()
    {
        machines = await machineService.GetMachinesAsync();
    }
}

