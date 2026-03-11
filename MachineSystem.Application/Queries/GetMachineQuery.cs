using MachineSystem.Application.ViewModels;

namespace MachineSystem.Application.Queries;

public record GetMachineQuery(Guid MachineId);

public record GetMachineResult(MachineDetail Machine);