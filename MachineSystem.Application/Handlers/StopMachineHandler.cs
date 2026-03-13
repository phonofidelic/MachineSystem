using MachineSystem.Application.Commands;
using MachineSystem.Application.Repositories;
using MachineSystem.Application.ServiceContracts;
using MachineSystem.Application.Services.MachineService.Exceptions;

namespace MachineSystem.Application.Handlers;

public class StopMachineHandler(
    IMachineRepository machineRepository,
    IMachineService machineService,
    IUnitOfWork unitOfWork
) : IHandler<StopMachineCommand, MachineActionResult>
{
    private readonly IUnitOfWork unitOfWork = unitOfWork;
    public async Task<MachineActionResult> HandleAsync(StopMachineCommand command)
    {
        try
        {
            var machine = await machineRepository.GetMachineAsync(command.MachineId) ?? throw new MachineNotFoundException();

            // Simulate request to a physical machine in MachineService implementation.
            // Machine entity enforces invariants to ensure valid state.
            var status = await machineService.StopMachineAsync(machine);
            await unitOfWork.SaveAsync();

            return new MachineActionResult(status.IsOnline, status.IsOperational, status.IsRunning);
        } catch (Exception)
        {
            throw;
        }
    }
}
