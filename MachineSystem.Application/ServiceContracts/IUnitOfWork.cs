namespace MachineSystem.Application.ServiceContracts;

public interface IUnitOfWork
{
    public Task SaveAsync();
}
