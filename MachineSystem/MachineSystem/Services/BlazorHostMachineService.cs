using MachineSystem.Application.Services.MachineService;
using MachineSystem.Domain.Entities;
using MachineSystem.Domain.ValueObjects;

namespace MachineSystem.Services;

public class BlazorHostMachineService : IMachineService
{    
    private readonly Random random = new();

    public async Task<MachineStatus> StartMachineAsync(Machine machine)
    {
        await FakeDelay();

        return machine.Start();
    }

    public async Task<MachineStatus> StopMachineAsync(Machine machine)
    {
        await FakeDelay();

        return machine.Stop();
    }

    public async Task<MachineStatus> ConnectMachineAsync(Machine machine)
    {
        await FakeDelay();

        return machine.Connect();
    }

    public async Task<MachineStatus> DisconnectMachineAsync(Machine machine)
    {
        await FakeDelay();

        return machine.Disconnect();
    }

    private async Task FakeDelay()
    {
        int delayTime = random.Next(1, 5) * 1000;
        await Task.Delay(delayTime);
    }
}
