namespace MachineSystem.Domain.Exceptions;

public class MachineNameContainsDisallowedCharacterException()
    : BaseMachineException("Machine name may only contain alpha-numeric characters");
