using MachineSystem.Domain.ValueObjects;

namespace MachineSystem.Domain.Entities;

public class Machine : BaseCreatableEntity<Guid>
{
    public override Guid Id { get; set; } = Guid.NewGuid();

    public required string Name { get; set; }

    public required MachineType Type { get; set; }

    public MachineStatus Status { get; set; } = default!;

    public string? LastData { get; set; }

    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

    public void SetStatus(MachineStatus newStatus)
    {
        Status = newStatus.Clone();
    }

    public MachineStatus Start()
    {
        var currentStatus = Status.Clone();

        // Enforce invariants
        if (
            currentStatus.IsOnline && 
            currentStatus.IsOperational && 
            !currentStatus.IsRunning)
        {
            Status = new MachineStatus(
                isOnline: currentStatus.IsOnline,
                isOperational: currentStatus.IsOperational,
                isRunning: true
            );
        }

        return Status.Clone();
    }

    public MachineStatus Stop()
    {
        var currentStatus = Status.Clone();

        // Enforce invariants
        if (
            currentStatus.IsOnline && 
            currentStatus.IsOperational && 
            currentStatus.IsRunning)
        {
            Status = new MachineStatus(
                isOnline: currentStatus.IsOnline,
                isOperational: currentStatus.IsOperational,
                isRunning: false
            );
        }

        return Status.Clone();
    }

    public MachineStatus Connect()
    {
        var currentStatus = Status.Clone();

        // Enforce invariants
        if (
            !currentStatus.IsOnline && 
            currentStatus.IsOperational)
        {
            Status = new MachineStatus(
                isOnline: true,
                isOperational: currentStatus.IsOperational,
                isRunning: currentStatus.IsRunning
            );
        }

        return Status.Clone();
    }

    public MachineStatus Disconnect()
    {
        var currentStatus = Status.Clone();

        // Enforce invariants
        if (currentStatus.IsOnline)
        {
            Status = new MachineStatus(
                isOnline: false,
                isOperational: currentStatus.IsOperational,
                isRunning: false
            );
        }

        return Status.Clone();
    }
}
