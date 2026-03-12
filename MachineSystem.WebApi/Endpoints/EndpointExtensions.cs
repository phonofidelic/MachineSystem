using MachineSystem.Application;
using MachineSystem.Application.Commands;
using MachineSystem.Application.Queries;
using MachineSystem.Application.Services.MachineService;
using MachineSystem.Application.Services.MachineService.Exceptions;
using MachineSystem.Application.UseCases.StartMachine;

// ToDo: Move to separate project
namespace MachineSystem.WebApi.Endpoints;

public static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet(
            "/api/machines", 
            async (IHandler<GetMachinesQuery, GetMachinesResult> handler) => 
        {
            var result = await handler.HandleAsync(new GetMachinesQuery());
            return Results.Ok(result);
        });

        builder.MapGet(
            "/api/machines/{id:Guid}", 
            async (Guid id, IHandler<GetMachineQuery, GetMachineResult> handler) => 
        {
            var result = await handler.HandleAsync(new GetMachineQuery(id));
            return Results.Ok(result);
        });

        builder.MapPatch(
            "/api/machines/{id:Guid}/start", 
            async (Guid id, IHandler<StartMachineCommand, MachineActionResult> handler) => 
        {
            var result = await handler.HandleAsync(new StartMachineCommand(id));
            return Results.Ok(result);
        });

        builder.MapPatch(
            "/api/machines/{id:Guid}/stop", 
            async (Guid id, IHandler<StopMachineCommand, MachineActionResult> handler) => 
        {
            var result = await handler.HandleAsync(new StopMachineCommand(id));
            return Results.Ok(result);
        });

        builder.MapPatch(
            "/api/machines/{id:Guid}/connect", 
            async (Guid id, IHandler<ConnectMachineCommand, MachineActionResult> handler) => 
        {
            var result = await handler.HandleAsync(new ConnectMachineCommand(id));
            return Results.Ok(result);
        });

        builder.MapPatch("/api/machines/{id:Guid}/disconnect", async (Guid id, IMachineService machineService) => {
            try
            {
                await machineService.DisconnectMachineAsync(id);
                return Results.NoContent();
            } catch (Exception ex)
            {
                // ToDo: Log error
                if (ex is MachineNotFoundException)
                    return Results.NotFound();
                
                return Results.InternalServerError();
            }
        });

        return builder;
    }
}
