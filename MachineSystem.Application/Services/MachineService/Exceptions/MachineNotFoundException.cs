using MachineSystem.Api.Dtos;

namespace MachineSystem.Application.Services.MachineService.Exceptions;

public class MachineNotFoundException : Exception
{
    private static string DefaultMessage { get => "Machine not found"; }
    
    public override string Message { get; }
    
    public MachineNotFoundException(string message) : base(message)
    {
        Message = message;
    }

    public MachineNotFoundException() : base(DefaultMessage)
    {
        Message = DefaultMessage;
    }

    public MachineNotFoundExceptionDto ToExceptionDto()
    {
        return new MachineNotFoundExceptionDto(Message, 404);
    }
}
