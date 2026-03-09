namespace MachineSystem.Application.Services;

public interface IUnitOfWork
{
    public Task SaveAsync();
}
