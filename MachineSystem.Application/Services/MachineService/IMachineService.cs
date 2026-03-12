using MachineSystem.Domain.Entities;
using MachineSystem.Domain.ValueObjects;

namespace MachineSystem.Application.Services.MachineService;

public interface IMachineService
{
    public Task<List<Machine>> GetMachinesAsync();

    public Task<Machine> GetMachineAsync(Guid machineId);

    public Task<MachineStatus> StartMachineAsync(Machine machine);

    public Task<MachineStatus> StopMachineAsync(Machine machine);

    public Task ConnectMachineAsync(Guid machineId);

    public Task DisconnectMachineAsync(Guid machineId);
}
