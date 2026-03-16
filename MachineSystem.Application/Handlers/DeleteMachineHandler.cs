using System;
using MachineSystem.Application.Commands;
using MachineSystem.Application.Repositories;
using MachineSystem.Application.ServiceContracts;
using MachineSystem.Application.Services.MachineService.Exceptions;

namespace MachineSystem.Application.Handlers;

public class DeleteMachineHandler(
    IMachineRepository repository,
    IMachineService machineService,
    IUnitOfWork unitOfWork
) : IHandler<DeleteMachineCommand, DeleteMachineResult>
{
    private readonly IMachineRepository machineRepository = repository;

    private readonly IMachineService machineService = machineService;

    private readonly IUnitOfWork unitOfWork = unitOfWork;

    public async Task<DeleteMachineResult> HandleAsync(DeleteMachineCommand command)
    {
        var machine = await machineRepository.GetMachineAsync(command.MachineId)
            ?? throw new MachineNotFoundException();

        await machineService.UninstallAsync(machine);
        
        machineRepository.Remove(machine);

        await unitOfWork.SaveAsync();

        return new DeleteMachineResult(command.MachineId);
    }
}
