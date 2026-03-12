using MachineSystem.Application.ServiceContracts;

namespace MachineSystem.Infrastructure.Data;

public class UnitOfWork(ApplicationDbContext ctx) : IUnitOfWork
{
    private readonly ApplicationDbContext context = ctx;
    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }
}
