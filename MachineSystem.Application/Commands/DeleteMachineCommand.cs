using System;

namespace MachineSystem.Application.Commands;

public record DeleteMachineCommand(Guid MachineId);

public record DeleteMachineResult(Guid DeletedMachineId);
