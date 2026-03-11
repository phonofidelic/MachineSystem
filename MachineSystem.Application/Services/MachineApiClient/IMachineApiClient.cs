using MachineSystem.Application.UseCases.StartMachine;

namespace MachineSystem.Application.Services.MachineApiClient;

public interface IMachineApiClient
{
    public Task<GetMachinesResult> GetMachinesAsync(GetMachinesQuery query);

    public Task<GetMachineResult> GetMachineAsync(GetMachineQuery query);

    Task<StartMachineResult> StartMachineAsync(StartMachineCommand command);
}
