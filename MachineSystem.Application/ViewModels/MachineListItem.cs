using MachineSystem.Domain.ValueObjects;

namespace MachineSystem.Application.ViewModels;

public class MachineListItem
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public MachineStatus Status { get; set; } = default!;

    public string? LastData { get; set; }

    public DateTime LastUpdated { get; set; }
}
