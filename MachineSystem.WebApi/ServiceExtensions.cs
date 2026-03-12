using MachineSystem.Application.Commands;
using MachineSystem.Application.Handlers;
using MachineSystem.Application.Queries;
using MachineSystem.Application.ServiceContracts;
using MachineSystem.Application.UseCases.StartMachine;

namespace MachineSystem.WebApi;

public static class ServiceExtensions
{
    public static void AddScopedHandlers(this IServiceCollection services)
    {
        services.AddScoped<IHandler<GetMachinesQuery, GetMachinesResult>, GetMachinesHandler>();
        services.AddScoped<IHandler<GetMachineQuery, GetMachineResult>, GetMachineHandler>();
        services.AddScoped<IHandler<StartMachineCommand, MachineActionResult>, StartMachineHandler>();
        services.AddScoped<IHandler<StopMachineCommand, MachineActionResult>, StopMachineHandler>();
        services.AddScoped<IHandler<ConnectMachineCommand, MachineActionResult>, ConnectMachineHandler>();
        services.AddScoped<IHandler<DisconnectMachineCommand, MachineActionResult>, DisconnectMachineHandler>();
    }

    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                // ToDo: Add Blazor app origin
            });

            // ToDo: Remove AllowAll policy
            options.AddPolicy("AllowAll", policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
    }
}
