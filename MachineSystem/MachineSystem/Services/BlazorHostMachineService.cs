using MachineSystem.Application.Repositories;
using MachineSystem.Application.Services;
using MachineSystem.Application.Services.MachineService;
using MachineSystem.Application.Services.MachineService.Dtos;
using MachineSystem.Application.Services.MachineService.Exceptions;
using MachineSystem.Domain.Entities;
using MachineSystem.Domain.ValueObjects;

namespace MachineSystem.Services;

public class BlazorHostMachineService(
    IMachineRepository repo,
    IUnitOfWork unitOfWork) : IMachineService
{
    private readonly IMachineRepository machineRepository = repo;

    private readonly IUnitOfWork unitOfWork = unitOfWork;
    
    private readonly Random random = new();

    public async Task<List<Machine>> GetMachinesAsync()
    {
        return await machineRepository.GetMachinesAsync();
    }

    public async Task<Machine> GetMachineAsync(Guid machineId)
    {
        return await machineRepository.GetMachineAsync(machineId) ?? throw new MachineNotFoundException();
    }
    public async Task<MachineStatus> StartMachineAsync(Machine machine)
    {
        await FakeDelay();

        return machine.Start();
    }

    public async Task<MachineStatus> StopMachineAsync(Machine machine)
    {
        await FakeDelay();

        return machine.Stop();
    }

    public async Task<MachineStatus> ConnectMachineAsync(Machine machine)
    {
        await FakeDelay();

        return machine.Connect();
    }

    public async Task DisconnectMachineAsync(Guid machineId)
    {
        var machine = await machineRepository.GetMachineAsync(machineId) ?? throw new MachineNotFoundException();

        var previousMachineStatus = machine.Status.Clone();

        await FakeDelay();

        var newMachineStatus = new MachineStatus(
            isOnline: false,
            isOperational: previousMachineStatus.IsOperational,
            isRunning: previousMachineStatus.IsRunning
        );

        machine.SetStatus(newMachineStatus);

        await unitOfWork.SaveAsync();
    }

    private async Task FakeDelay()
    {
        int delayTime = random.Next(1, 5) * 1000;
        await Task.Delay(delayTime);
    }
}
