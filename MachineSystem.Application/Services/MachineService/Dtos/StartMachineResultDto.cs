namespace MachineSystem.Application.Services.MachineService.Dtos;

public record StartMachineResultDto(
    bool IsOnline,
    bool IsOperational,
    bool IsRunning);