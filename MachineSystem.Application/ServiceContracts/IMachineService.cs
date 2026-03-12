using MachineSystem.Domain.Entities;
using MachineSystem.Domain.ValueObjects;

namespace MachineSystem.Application.ServiceContracts;

public interface IMachineService
{
    public Task<MachineStatus> StartMachineAsync(Machine machine);

    public Task<MachineStatus> StopMachineAsync(Machine machine);

    public Task<MachineStatus> ConnectMachineAsync(Machine machine);

    public Task<MachineStatus> DisconnectMachineAsync(Machine machine);
}
