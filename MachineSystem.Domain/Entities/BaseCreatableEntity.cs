namespace MachineSystem.Domain.Entities;

public abstract class BaseCreatableEntity<TId> : BaseEntity<TId>
{
    public virtual DateTime CreatedAt { get; set; }
}
