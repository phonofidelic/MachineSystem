using MachineSystem.Application.Repositories;
using MachineSystem.Application.Services.MachineService;
using MachineSystem.Application.Services.MachineService.Exceptions;

namespace MachineSystem.Application.UseCases.StartMachine;

public class StartMachineHandler(
    IMachineRepository machineRepository,
    IMachineService machineService) : IHandler<StartMachineCommand, StartMachineResult>
{
    private readonly IMachineRepository machineRepository = machineRepository;
    private readonly IMachineService machineService = machineService;

    private readonly Random random = new();

    public async Task<StartMachineResult> HandleAsync(StartMachineCommand command)
    {
        try
        {
            var machine = await machineRepository.GetMachineAsync(command.machineId) ?? throw new MachineNotFoundException();

            // Machine entity enforces invariants to ensure valid state
            machine.Start();

            // ToDo: Simulate request to a physical machine
            await Task.Delay(random.Next(0, 3) * 1000);
            await machineService.StartMachineAsync()


        } catch (Exception)
        {

        }
    }
}
