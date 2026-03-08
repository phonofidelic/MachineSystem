using MachineSystem.Domain.Entities;
using MachineSystem.Domain.Services;
using MachineSystem.Domain.ValueObjects;

namespace MachineSystem.Client.Services;

public class MockClientMachineService : IMachineService
{
    private List<Machine> machines = [
            new Machine
            {
                Name = "Machine 01",
                Status = new MachineStatus(
                    isOnline: true,
                    isOperational: true,
                    isRunning: true),
                LastData = "Temp: 25c"
            },
            new Machine
            {
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
        await Task.Delay(2000);
        return machines;
    }

    public async Task StartMachineAsync(Guid machineId)
    {
        var machine = machines.Find(m => m.Id == machineId) ?? throw new Exception("Machine not found");

        if (!CanStartMachine(machine)) return;

        var previousMachineStatus = machine.Status.Clone();

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

        machine.Status = new MachineStatus(
            isOnline: previousMachineStatus.IsOnline,
            isOperational: previousMachineStatus.IsOperational,
            isRunning: false
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
}
