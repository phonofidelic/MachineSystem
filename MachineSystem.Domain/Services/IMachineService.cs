using MachineSystem.Domain.Entities;

namespace MachineSystem.Domain.Services;

public interface IMachineService
{
    public Task<List<Machine>> GetMachinesAsync();
    
    public Task StartMachineAsync(Guid machineId);

    public Task StopMachineAsync(Guid machineId);
}
