namespace MachineSystem.Application.Commands;

public record MachineActionResult(
    bool IsOnline,
    bool IsOperational,
    bool IsRunning);