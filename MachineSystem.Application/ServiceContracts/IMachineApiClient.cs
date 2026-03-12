using MachineSystem.Application.Commands;
using MachineSystem.Application.Queries;

namespace MachineSystem.Application.ServiceContracts;

public interface IMachineApiClient
{
    public Task<GetMachinesResult> GetMachinesAsync(GetMachinesQuery query);

    public Task<GetMachineResult> GetMachineAsync(GetMachineQuery query);

    public Task<MachineActionResult> StartMachineAsync(StartMachineCommand command);

    public Task<MachineActionResult> StopMachineAsync(StopMachineCommand command);

    public Task<MachineActionResult> ConnectMachineAsync(ConnectMachineCommand command);

    public Task<MachineActionResult> DisconnectMachineAsync(DisconnectMachineCommand command);
}
