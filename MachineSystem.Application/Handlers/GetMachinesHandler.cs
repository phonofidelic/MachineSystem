using MachineSystem.Application.Queries;
using MachineSystem.Application.Repositories;
using MachineSystem.Application.ServiceContracts;
using MachineSystem.Application.ViewModels;

namespace MachineSystem.Application.Handlers;

public class GetMachinesHandler(
    IMachineRepository repository
) : IHandler<GetMachinesQuery, GetMachinesResult>
{
    private readonly IMachineRepository machineRepository = repository;
    public async Task<GetMachinesResult> HandleAsync(GetMachinesQuery request)
    {
        var machines = machineRepository.FindAll();

        var items = machines.Select(m => new MachineListItem
        {
            Id = m.Id,
            Name = m.Name,
            Type = m.Type.Name,
            Status = m.Status.Clone(),
            LastData = m.LastData,
            LastUpdated = m.LastUpdated,
            CreatedAt = m.CreatedAt
        }).ToList();

        return new GetMachinesResult(items);
    }
}
