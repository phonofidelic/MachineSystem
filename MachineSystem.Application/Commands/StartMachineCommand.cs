namespace MachineSystem.Application.UseCases.StartMachine;

public record StartMachineCommand(Guid MachineId);

public record StartMachineResult(
    bool IsOnline,
    bool IsOperational,
    bool IsRunning);