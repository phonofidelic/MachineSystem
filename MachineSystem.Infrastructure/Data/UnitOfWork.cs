using MachineSystem.Application.ServiceContracts;

namespace MachineSystem.Infrastructure.Data;

public class UnitOfWork(ApplicationDbContext ctx) : IUnitOfWork
{
    ApplicationDbContext context = ctx;
    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }
}
