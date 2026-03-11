using MachineSystem.Application.Services.MachineService.Dtos;
using MachineSystem.Domain.Entities;

namespace MachineSystem.Application.Services.MachineService;

public interface IMachineService
{
    public Task<List<Machine>> GetMachinesAsync();

    public Task<Machine> GetMachineAsync(Guid machineId);

    public Task<StartMachineResultDto> StartMachineAsync(StartMachineCommandDto command);

    public Task StopMachineAsync(Guid machineId);

    public Task ConnectMachineAsync(Guid machineId);

    public Task DisconnectMachineAsync(Guid machineId);
}
