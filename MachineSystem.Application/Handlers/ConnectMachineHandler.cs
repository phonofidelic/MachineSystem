using MachineSystem.Application.Commands;
using MachineSystem.Application.Repositories;
using MachineSystem.Application.Services.MachineService;
using MachineSystem.Application.Services.MachineService.Exceptions;

namespace MachineSystem.Application.Handlers;

public class ConnectMachineHandler(
    IMachineRepository machineRepository,
    IMachineService machineService) : IHandler<ConnectMachineCommand, MachineActionResult>
{
    private readonly IMachineRepository machineRepository = machineRepository;
    
    private readonly IMachineService machineService = machineService;

    public async Task<MachineActionResult> HandleAsync(ConnectMachineCommand command)
    {
        try
        {
            var machine = await machineRepository.GetMachineAsync(command.MachineId) ?? throw new MachineNotFoundException();

            // Simulate request to a physical machine in MachineService implementation.
            // Machine entity enforces invariants to ensure valid state.
            var status = await machineService.ConnectMachineAsync(machine);

            return new MachineActionResult(status.IsOnline, status.IsOperational, status.IsRunning);
        } catch (Exception)
        {
            throw;
        }
    }
}
