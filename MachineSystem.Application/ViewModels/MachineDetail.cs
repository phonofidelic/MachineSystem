using System;
using MachineSystem.Domain.ValueObjects;

namespace MachineSystem.Application.ViewModels;

public class MachineDetail
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public MachineType Type { get; set; } = default!;

    public string? LastData { get; set; }

    public DateTime LastUpdated { get; set; }
}
