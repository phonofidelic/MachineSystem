
using Bogus;
using MachineSystem.Domain.Entities;
using MachineSystem.Domain.ValueObjects;
using Microsoft.Extensions.DependencyInjection;

namespace MachineSystem.Infrastructure.Data;

public static class Seeder
{
    
    public static List<Machine> GenerateMachines(int count)
    {
        Randomizer.Seed = new Random(2435);
        MachineType[] machineTypes = [
            new MachineType("SCARA"),
            new MachineType("Delta"),
            new MachineType("Cartesian"),
        ];

        var machineGenerator = new Faker<Machine>()
            .RuleFor(m => m.Id, f => f.Random.Guid())
            .RuleFor(m => m.Name, f => $"{f.Name.FirstName()} 0{f.Random.Int(0, 9)}{f.Random.Int(0, 9)}")
            .RuleFor(m => m.Type, f => f.PickRandom(machineTypes))
            .RuleFor(m => m.Status, f => new MachineStatus(
                isOnline: f.PickRandom(true, false), 
                isOperational: true, 
                isRunning: f.PickRandom(true, false)))
            .RuleFor(m => m.LastData, f => f.Hacker.IngVerb())
            .RuleFor(m => m.LastUpdated, f => f.Date.Recent());

        return machineGenerator.Generate(count);
    }

    public static async Task SeedDatabase(int machineCount, IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

        var machines = GenerateMachines(machineCount);

        await context.Machines.AddRangeAsync(machines);
        await context.SaveChangesAsync();
    }
}
