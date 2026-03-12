using MachineSystem.Application.Queries;
using MachineSystem.Application.Repositories;
using MachineSystem.Application.ServiceContracts;
using MachineSystem.Application.Services.MachineService.Exceptions;
using MachineSystem.Application.ViewModels;

namespace MachineSystem.Application.Handlers;

public class GetMachineHandler(IMachineRepository repository) : IHandler<GetMachineQuery, GetMachineResult>
{
    private readonly IMachineRepository repository = repository;
    public async Task<GetMachineResult> HandleAsync(GetMachineQuery query)
    {
        var machine = await repository.GetMachineAsync(query.MachineId) ?? throw new MachineNotFoundException();

        var machineDetail = new MachineDetail
        {
            Id = machine.Id,
            Name = machine.Name,
            Type = machine.Type,
            LastData = machine.LastData,
            LastUpdated = machine.LastUpdated  
        };

        return new GetMachineResult(machineDetail);
    }
}
