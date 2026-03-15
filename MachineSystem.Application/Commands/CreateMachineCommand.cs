using MachineSystem.Application.ViewModels;
using MachineSystem.Domain.ValueObjects;

namespace MachineSystem.Application.Commands;

public record CreateMachineCommand(string Name, MachineType Type);

public record CreateMachineResult(MachineListItem CreatedMachine);
