using System;

namespace MachineSystem.Domain.Exceptions;

[Serializable]
public class BaseMachineException : Exception
{
    public BaseMachineException(string message) : base(message)
    {
    }

    public BaseMachineException(string message, Exception? innerException) : base(message, innerException)
    {
    }
}