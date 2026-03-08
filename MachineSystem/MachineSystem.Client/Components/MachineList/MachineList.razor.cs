
using MachineSystem.Client.Services;
using MachineSystem.Domain.Entities;
using MachineSystem.Domain.Services;
using Microsoft.AspNetCore.Components;

namespace MachineSystem.Client.Components.MachineList;

public partial class MachineList
{
    private readonly IMachineService machineService = new MockClientMachineService();
    private List<Machine> machines = [];

    [Parameter]
    public bool IsLoading { get; set; } = true;

    protected async override Task OnInitializedAsync()
    {
        machines = await machineService.GetMachinesAsync();
        IsLoading = false;
    }
}

