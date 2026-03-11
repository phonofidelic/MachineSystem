using MachineSystem.Application.ViewModels;

namespace MachineSystem.Application.Queries;

public record GetMachinesQuery();

public record GetMachinesResult(IEnumerable<MachineListItem> Machines);
