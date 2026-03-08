using System;

namespace MachineSystem.Domain.ValueObjects;

public class MachineStatus(bool isOnline, bool isRunning) : ValueObject
{
    public bool IsOnline { get; set; } = isOnline;

    public bool IsRunning { get; set; } = isRunning;

    public override string ToString()
    {
        return IsOnline ? "online" : "offline";
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return IsOnline;
        yield return IsRunning;
    }
}
