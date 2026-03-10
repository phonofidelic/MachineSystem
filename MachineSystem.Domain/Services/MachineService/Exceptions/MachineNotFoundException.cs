using System;

namespace MachineSystem.Domain.Services.MachineService.Exceptions;

public class MachineNotFoundException : Exception, IMachineServiceExceptionProvider
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
}
