using MachineSystem.Domain.ValueObjects;

namespace MachineSystem.Domain.Entities;

public class Machine
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public required string Name { get; set; }

    public required MachineStatus Status { get; set; }

    public string? LastData { get; set; }

    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}
