using MachineSystem.Application.Commands;
using MachineSystem.Application.Queries;
using MachineSystem.Application.UseCases.StartMachine;

namespace MachineSystem.Application.Services.MachineApiClient;

public interface IMachineApiClient
{
    public Task<GetMachinesResult> GetMachinesAsync(GetMachinesQuery query);

    public Task<GetMachineResult> GetMachineAsync(GetMachineQuery query);

    Task<MachineActionResult> StartMachineAsync(StartMachineCommand command);
}
