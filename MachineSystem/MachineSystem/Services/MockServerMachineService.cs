using System;
using MachineSystem.Domain.Entities;
using MachineSystem.Domain.Services;
using MachineSystem.Domain.ValueObjects;

namespace MachineSystem.Services;

public class MockServerMachineService : IMachineService
{
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
