namespace MachineSystem.Api.Dtos;

public record MachineNotFoundExceptionDto(
    string message,
    int statusCode);
