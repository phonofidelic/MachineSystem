namespace MachineSystem.Domain.ValueObjects;

public class MachineStatus(bool isOnline, bool isOperational, bool isRunning) : ValueObject
{
    public bool IsOnline { get; set; } = isOnline;

    public bool IsOperational { get; set; } = isOperational;

    public bool IsRunning { get; set; } = isRunning;

    public MachineStatus Clone() => new(IsOnline, IsOperational, IsRunning);

    public override string ToString()
    {
        return IsOnline ? "online" : "offline";
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return IsOnline;
        yield return IsOperational;
        yield return IsRunning;
    }
}
