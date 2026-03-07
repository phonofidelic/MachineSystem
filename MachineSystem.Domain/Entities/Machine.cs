using System;

namespace MachineSystem.Domain.Entities;

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