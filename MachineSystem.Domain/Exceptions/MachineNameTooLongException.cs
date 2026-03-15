namespace MachineSystem.Domain.Exceptions;

public class MachineNameTooLongException() 
    : BaseMachineException("Machine name cannot be longer than 30 characters");
