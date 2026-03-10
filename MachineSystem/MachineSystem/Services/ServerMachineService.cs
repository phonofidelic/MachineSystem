using System;
using MachineSystem.Application.Repositories;
using MachineSystem.Application.Services;
using MachineSystem.Domain.Entities;
using MachineSystem.Domain.Services;
using MachineSystem.Domain.ValueObjects;

namespace MachineSystem.Services;

public class ServerMachineService(
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

    public async Task<Machine>? GetMachineAsync(Guid machineId)
    {
        return await machineRepository.GetMachineAsync(machineId) ?? throw new Exception("Machine not found");
    }
    public async Task StartMachineAsync(Guid machineId)
    {
        var machine = await machineRepository.GetMachineAsync(machineId) ?? throw new Exception("Machine not found");

        if (!CanStartMachine(machine)) return;

        var previousMachineStatus = machine.Status.Clone();

        await FakeDelay();

        machine.Status = new MachineStatus(
            isOnline: previousMachineStatus.IsOnline,
            isOperational: previousMachineStatus.IsOperational,
            isRunning: true
        );

        await unitOfWork.SaveAsync();
    }

    public async Task StopMachineAsync(Guid machineId)
    {
        var machine = await machineRepository.GetMachineAsync(machineId) ?? throw new Exception("Machine not found");

        if (!CanStopMachine(machine)) return;

        var previousMachineStatus = machine.Status.Clone();

        await FakeDelay();

        machine.Status = new MachineStatus(
            isOnline: previousMachineStatus.IsOnline,
            isOperational: previousMachineStatus.IsOperational,
            isRunning: false
        );

        await unitOfWork.SaveAsync();
    }

    public async Task ConnectMachineAsync(Guid machineId)
    {
        var machine = await machineRepository.GetMachineAsync(machineId) ?? throw new Exception("Machine not found");

        var previousMachineStatus = machine.Status.Clone();

        await FakeDelay();
        
        machine.Status = new MachineStatus(
            isOnline: true,
            isOperational: previousMachineStatus.IsOperational,
            isRunning: previousMachineStatus.IsRunning
        );

        await unitOfWork.SaveAsync();
    }

    public async Task DisconnectMachineAsync(Guid machineId)
    {
        var machine = await machineRepository.GetMachineAsync(machineId) ?? throw new Exception("Machine not found");

        var previousMachineStatus = machine.Status.Clone();

        await FakeDelay();
        
        machine.Status = new MachineStatus(
            isOnline: false,
            isOperational: previousMachineStatus.IsOperational,
            isRunning: previousMachineStatus.IsRunning
        );

        await unitOfWork.SaveAsync();
    }

    private static bool CanStartMachine(Machine machine)
    {
        if (machine.Status.IsRunning) return false;

        if (!machine.Status.IsOperational)
            throw new Exception("Machine is not operational");

        if (!machine.Status.IsOnline)
            throw new Exception("Could not connect to machine");

        return true;
    }

    private static bool CanStopMachine(Machine machine)
    {
        if (!machine.Status.IsRunning) return false;

        if (!machine.Status.IsOperational)
            throw new Exception("Machine is not operational");

        if (!machine.Status.IsOnline)
            throw new Exception("Could not connect to machine");

        return true;
    }

    private async Task FakeDelay()
    {
        int delayTime = random.Next(0, 5) * 1000;
        await Task.Delay(delayTime);
    }
}
