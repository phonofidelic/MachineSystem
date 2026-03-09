using MachineSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MachineSystem.Infrastructure.Data;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Machine> Machines { get; set; }

    public override int SaveChanges()
    {
        AddTimestamps();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        AddTimestamps();
        return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    private void AddTimestamps()
    {
        var now = DateTime.UtcNow;

        var creatableEntries = ChangeTracker.Entries()
            .Where(e => e.Entity is BaseCreatableEntity<Guid> && e.State == EntityState.Added);

        foreach (var entry in creatableEntries)
        {
            ((BaseCreatableEntity<Guid>)entry.Entity).CreatedAt = now;
        }

        var updateableEntries = ChangeTracker.Entries()
            .Where(e => e.Entity is Machine && e.State == EntityState.Modified);

        foreach (var entry in updateableEntries)
        {
            ((Machine)entry.Entity).LastUpdated = now;
        }
    }
}
