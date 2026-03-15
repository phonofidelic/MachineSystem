using MachineSystem.Application.Commands;
using MachineSystem.Application.Repositories;
using MachineSystem.Application.ServiceContracts;
using MachineSystem.Application.ViewModels;
using MachineSystem.Domain.Entities;

namespace MachineSystem.Application.Handlers;

public class CreateMachineHandler(
    IMachineRepository repository,
    IMachineService machineService,
    IUnitOfWork unitOfWork
) : IHandler<CreateMachineCommand, CreateMachineResult>
{
    private readonly IMachineRepository machineRepository = repository;

    private readonly IMachineService machineService = machineService;

    private readonly IUnitOfWork unitOfWork = unitOfWork;

    public async Task<CreateMachineResult> HandleAsync(CreateMachineCommand command)
    {
        var machine = new Machine
        {
            Name = command.Name,
            Type = command.Type
        };

        await machineRepository.AddAsync(machine);
        await machineService.ConnectMachineAsync(machine);

        await unitOfWork.SaveAsync();

        // ToDo: Move view model implementation to API endpoint
        var machineListItem = new MachineListItem
        {
            Id = machine.Id,
            Name = machine.Name,
            Type = machine.Type.ToString(),
            Status = machine.Status.Clone(),
            LastData = machine.LastData,
            LastUpdated = machine.LastUpdated
        };

        return new CreateMachineResult(machineListItem);
    }
}
