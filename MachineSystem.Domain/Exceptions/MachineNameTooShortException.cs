namespace MachineSystem.Domain.Exceptions;

public class MachineNameTooShortException() 
    : BaseMachineException("Machine name must be at least 3 characters long");
