using MachineSystem.Application.Repositories;
using MachineSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MachineSystem.Infrastructure.Data;

public class MachineRepository(ApplicationDbContext ctx) : IMachineRepository
{
    private readonly ApplicationDbContext context = ctx;
    public async Task<Machine?> GetMachineAsync(Guid machineId)
    {
        return await context.Machines.FindAsync(machineId);
    }

    public async Task<List<Machine>> GetMachinesAsync()
    {
        // ToDo: Implement a PagingService
        return await context.Machines.Take(50).ToListAsync();
    }
}
