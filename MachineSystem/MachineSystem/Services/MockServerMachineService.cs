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
        List<Machine> machines = [
            new Machine
            {
                Name = "Machine 03",
                Status = new MachineStatus(true, true),
                LastData = "Temp: 25c"
            },
            new Machine
            {
                Name = "Machine 04",
                LastData = "Temp: 15c"
            }
        ];

        await Task.Delay(2000);
        return machines;
    }
}
