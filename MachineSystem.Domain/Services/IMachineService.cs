using MachineSystem.Domain.Entities;

namespace MachineSystem.Domain.Services;

public interface IMachineService
{
    public Task<List<Machine>> GetMachinesAsync();
}
