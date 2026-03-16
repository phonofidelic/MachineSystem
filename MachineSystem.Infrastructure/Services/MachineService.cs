using MachineSystem.Application.ServiceContracts;
using MachineSystem.Domain.Entities;
using MachineSystem.Domain.ValueObjects;

namespace MachineSystem.Infrastructure.Services;

public class MachineService : IMachineService
{    
    private readonly Random random = new();

    public async Task InstallAsync(Machine machine)
    {
        await FakeDelay();

        machine.Initialize();
    }

    public async Task UninstallAsync(Machine machine)
    {
        await FakeDelay();

        if (machine.Status.IsRunning)
            machine.Stop();

        machine.Disconnect();
    }

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