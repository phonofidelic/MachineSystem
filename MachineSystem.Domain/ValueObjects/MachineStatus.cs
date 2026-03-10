namespace MachineSystem.Domain.ValueObjects;

public class MachineStatus : ValueObject
{
    public bool IsOnline { get; init; }

    public bool IsOperational { get; init; }

    public bool IsRunning { get; init; }

    public MachineStatus()
    {
        IsOnline = false;
        IsOperational = true;
        IsRunning = false;
    }

    public MachineStatus(bool isOnline, bool isOperational, bool isRunning) : base()
    {
        IsOnline = isOnline;
        IsOperational = isOperational;
        IsRunning = isRunning;
    }

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
