
using MachineSystem.Domain.Entities;

namespace MachineSystem.Client.Components.MachineList;

public partial class MachineList
{
    private List<Machine> machines = [
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
}

