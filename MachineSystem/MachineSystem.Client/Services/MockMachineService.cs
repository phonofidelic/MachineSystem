using MachineSystem.Domain.Entities;
using MachineSystem.Domain.Services;

namespace MachineSystem.Client.Services;

public class MockMachineService : IMachineService
{
    public async Task<List<Machine>> GetMachinesAsync()
    {
        List<Machine> machines = [
            new Machine
            {
                Name = "Machine 01",
                Status = MachineStatus.Online,
                LastData = "Temp: 25c"
            },
            new Machine
            {
                Name = "Machine 02",
                LastData = "Temp: 15c"
            }
        ];

        await Task.Delay(2000);
        return machines;
    }
}
