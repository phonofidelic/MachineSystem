namespace MachineSystem.Application.UseCases.StartMachine;

public record StartMachineResult(
    bool IsOnline,
    bool IsOperational,
    bool IsRunning);
