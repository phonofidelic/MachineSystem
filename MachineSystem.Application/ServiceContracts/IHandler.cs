namespace MachineSystem.Application.ServiceContracts;

public interface IHandler<TRequest, TResult>
{
    public Task<TResult> HandleAsync(TRequest request);
}
