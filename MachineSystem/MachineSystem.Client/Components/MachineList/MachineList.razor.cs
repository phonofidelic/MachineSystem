
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

public class Machine
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public required string Name { get; set; }

    public MachineStatus Status { get; set; }

    public string? LastData { get; set; }

    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}

public enum MachineStatus
{
    Offline,
    Online
}