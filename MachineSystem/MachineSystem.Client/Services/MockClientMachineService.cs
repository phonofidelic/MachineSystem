using MachineSystem.Domain.Entities;
using MachineSystem.Domain.Services;
using MachineSystem.Domain.ValueObjects;

namespace MachineSystem.Client.Services;

public class MockClientMachineService : IMachineService
{
    private Random random = new();
    private List<Machine> machines = [
            new Machine
            {
                Id = Guid.Parse("2ceab771-9035-4795-a4ed-2009aa2962c8"),
                Name = "Machine 01",
                Status = new MachineStatus(
                    isOnline: true,
                    isOperational: true,
                    isRunning: true),
                LastData = "Temp: 25c"
            },
            new Machine
            {
                Id = Guid.Parse("f21c3814-6364-4a78-882b-1a00e7e16781"),
                Name = "Machine 02",
                Status = new MachineStatus(
                    isOnline: false,
                    isOperational: true,
                    isRunning: false),
                LastData = "Temp: 15c"
            }
        ];
    public async Task<List<Machine>> GetMachinesAsync()
    {
        // ToDo: use HttpClient to fetch data from web API
        await FakeDelay();
        return machines;
    }

    public async Task<Machine?> GetMachineAsync(Guid machineId)
    {
        return machines.Find(m => m.Id == machineId);
    }

    public async Task StartMachineAsync(Guid machineId)
    {
        var machine = machines.Find(m => m.Id == machineId) ?? throw new Exception("Machine not found");

        if (!CanStartMachine(machine)) return;

        var previousMachineStatus = machine.Status.Clone();

        await FakeDelay();

        machine.Status = new MachineStatus(
            isOnline: previousMachineStatus.IsOnline,
            isOperational: previousMachineStatus.IsOperational,
            isRunning: true
        );
    }

    public async Task StopMachineAsync(Guid machineId)
    {
        var machine = machines.Find(m => m.Id == machineId) ?? throw new Exception("Machine not found");

        if (!CanStopMachine(machine)) return;

        var previousMachineStatus = machine.Status.Clone();

        await FakeDelay();

        machine.Status = new MachineStatus(
            isOnline: previousMachineStatus.IsOnline,
            isOperational: previousMachineStatus.IsOperational,
            isRunning: false
        );
    }

    public async Task ConnectMachineAsync(Guid machineId)
    {
        var machine = machines.Find(m => m.Id == machineId) ?? throw new Exception("Machine not found");

        var previousMachineStatus = machine.Status.Clone();

        await FakeDelay();
        
        machine.Status = new MachineStatus(
            isOnline: true,
            isOperational: previousMachineStatus.IsOperational,
            isRunning: previousMachineStatus.IsRunning
        );
    }

    public async Task DisconnectMachineAsync(Guid machineId)
    {
        var machine = machines.Find(m => m.Id == machineId) ?? throw new Exception("Machine not found");

        var previousMachineStatus = machine.Status.Clone();

        await FakeDelay();
        
        machine.Status = new MachineStatus(
            isOnline: false,
            isOperational: previousMachineStatus.IsOperational,
            isRunning: previousMachineStatus.IsRunning
        );
    }

    private static bool CanStartMachine(Machine machine)
    {
        if (machine.Status.IsRunning) return false;

        if (!machine.Status.IsOperational)
            throw new Exception("Machine is not operational");

        if (!machine.Status.IsOnline)
            throw new Exception("Could not connect to machine");

        return true;
    }

    private static bool CanStopMachine(Machine machine)
    {
        if (!machine.Status.IsRunning) return false;

        if (!machine.Status.IsOperational)
            throw new Exception("Machine is not operational");

        if (!machine.Status.IsOnline)
            throw new Exception("Could not connect to machine");

        return true;
    }

    private async Task FakeDelay()
    {
        int delayTime = random.Next(0, 5) * 1000;
        await Task.Delay(delayTime);
    }
}
