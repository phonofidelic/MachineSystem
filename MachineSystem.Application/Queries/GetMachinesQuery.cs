using MachineSystem.Application.ViewModels;

namespace MachineSystem.Application.Queries;

public record GetMachinesQuery();

public record GetMachinesResult(List<MachineListItem> Machines);
