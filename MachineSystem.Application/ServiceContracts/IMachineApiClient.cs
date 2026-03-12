using MachineSystem.Application.Commands;
using MachineSystem.Application.Queries;

namespace MachineSystem.Application.ServiceContracts;

public interface IMachineApiClient
{
    public Task<GetMachinesResult> GetMachinesAsync(GetMachinesQuery query);

    public Task<GetMachineResult> GetMachineAsync(GetMachineQuery query);

    Task<MachineActionResult> StartMachineAsync(StartMachineCommand command);
}
