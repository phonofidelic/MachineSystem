
using MachineSystem.Domain.Entities;

namespace MachineSystem.Application.Repositories;

public interface IMachineRepository
{
    public Task<List<Machine>> GetMachinesAsync();

    public Task<Machine?> GetMachineAsync(Guid machineId);

    public IQueryable<Machine> FindAll(bool trackChanges = false);
}
