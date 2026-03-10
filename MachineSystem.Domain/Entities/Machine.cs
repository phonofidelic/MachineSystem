using MachineSystem.Domain.ValueObjects;

namespace MachineSystem.Domain.Entities;

public class Machine : BaseCreatableEntity<Guid>
{
    public override Guid Id { get; protected init; }

    private MachineStatus status { get; set; }

    public Machine(MachineStatus? initialStatus = null) : base()
    {
        Id = Guid.NewGuid();
        status = initialStatus ?? new();
    }

    public Machine(Guid id, MachineStatus? initialStatus = null) : base()
    {
        Id = id;
        status = initialStatus ?? new();
    }

    public required string Name { get; set; }

    public required MachineType Type { get; set; }

    public MachineStatus Status { get => status.Clone(); }

    public string? LastData { get; set; }

    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

    public void SetStatus(MachineStatus newStatus)
    {
        status = newStatus.Clone();
    }
}
