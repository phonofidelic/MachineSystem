namespace MachineSystem.Domain.ValueObjects;

public class MachineType(string name) : ValueObject
{
    public string Name { get; set; } = name;
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
}
