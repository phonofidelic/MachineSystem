
using Bogus;
using MachineSystem.Domain.Entities;

namespace MachineSystem.Infrastructure.Data;

public static class Seeder
{
    public static async Task GenerateMachines(int count)
    {
        throw new NotImplementedException();
        //var machineGenerator = new Faker<Machine>()
        //    .RuleFor(m => m.Name, "")
    }
}
