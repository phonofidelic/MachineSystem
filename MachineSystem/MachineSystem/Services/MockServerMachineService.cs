using MachineSystem.Domain.Entities;
using MachineSystem.Domain.Services.MachineService;

namespace MachineSystem.Services;

public class MockServerMachineService : IMachineService
{
    public Task ConnectMachineAsync(Guid machineId)
    {
        throw new NotImplementedException();
    }

    public Task DisconnectMachineAsync(Guid machineId)
    {
        throw new NotImplementedException();
    }

    public Task<Machine> GetMachineAsync(Guid machineId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Machine>> GetMachinesAsync()
    {
        // ToDo: use DbContext to query data from web database
        throw new NotImplementedException();
    }

    public Task StartMachineAsync(Guid machineId)
    {
        throw new NotImplementedException();
    }

    public Task StopMachineAsync(Guid machineId)
    {
        throw new NotImplementedException();
    }
}
