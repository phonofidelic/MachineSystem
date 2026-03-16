using System.Text.RegularExpressions;
using MachineSystem.Domain.Exceptions;
using MachineSystem.Domain.ValueObjects;

namespace MachineSystem.Domain.Entities;

public partial class Machine : BaseCreatableEntity<Guid>
{
    private bool IsInitialized { get; set; } = false;
    public override Guid Id { get; set; } = Guid.NewGuid();

    public required string Name { get; set; }

    public required MachineType Type { get; set; }

    public MachineStatus Status { get; set; } = default!;

    public string? LastData { get; set; }

    public DateTime LastUpdated { get; set; }

    public void Initialize()
    {
        EnforceInvariants();

        if (IsInitialized)
            return;

        var idString = Id.ToString();

        Name += $" {idString.Substring(idString.Length - 4)}";

        Status = new MachineStatus(
            isOnline: true,
            isOperational: true,
            isRunning: false
        );
    }

    public MachineStatus Start()
    {
        var currentStatus = Status.Clone();

        EnforceInvariants();

        if (
            currentStatus.IsOnline && 
            currentStatus.IsOperational && 
            !currentStatus.IsRunning)
        {
            Status = new MachineStatus(
                isOnline: currentStatus.IsOnline,
                isOperational: currentStatus.IsOperational,
                isRunning: true
            );
        }

        return Status.Clone();
    }

    public MachineStatus Stop()
    {
        var currentStatus = Status.Clone();

        EnforceInvariants();

        if (
            currentStatus.IsOnline && 
            currentStatus.IsOperational && 
            currentStatus.IsRunning)
        {
            Status = new MachineStatus(
                isOnline: currentStatus.IsOnline,
                isOperational: currentStatus.IsOperational,
                isRunning: false
            );
        }

        return Status.Clone();
    }

    public MachineStatus Connect()
    {
        var currentStatus = Status.Clone();

        EnforceInvariants();

        if (
            !currentStatus.IsOnline && 
            currentStatus.IsOperational)
        {
            Status = new MachineStatus(
                isOnline: true,
                isOperational: currentStatus.IsOperational,
                isRunning: currentStatus.IsRunning
            );
        }

        return Status.Clone();
    }

    public MachineStatus Disconnect()
    {
        var currentStatus = Status.Clone();

        EnforceInvariants();

        if (currentStatus.IsOnline)
        {
            Status = new MachineStatus(
                isOnline: false,
                isOperational: currentStatus.IsOperational,
                isRunning: false
            );
        }

        return Status.Clone();
    }

    private void EnforceInvariants()
    {
        ValidateMachineName(Name);
    }

    private static void ValidateMachineName(string name)
    {
        if (name.Length < 3)
            throw new MachineNameTooShortException();

        if (name.Length > 30)
            throw new MachineNameTooLongException();

        var allowedCharacters = AllowedMachineNameCharactersRegex();
        if (!allowedCharacters.IsMatch(name))
        {
            throw new MachineNameContainsDisallowedCharacterException();
        }
    }

    [GeneratedRegex(@"[A-Za-z][A-Za-z0-9\-]*")]
    private static partial Regex AllowedMachineNameCharactersRegex();
}