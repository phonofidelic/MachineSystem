using MachineSystem.Application.Services.MachineService;
using MachineSystem.Application.Services.MachineService.Dtos;
using MachineSystem.Application.Services.MachineService.Exceptions;

// ToDo: Move to separate project
namespace MachineSystem.Api.Extensions;

public static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/api/machines", async (IMachineService machineService) => {
            try
            {
                var machines = await machineService.GetMachinesAsync();
                return Results.Ok(machines);
            } catch (Exception ex)
            {
                // ToDo: Log error
                if (ex is MachineNotFoundException)
                {
                    return Results.NotFound();
                }

                return Results.InternalServerError();
            }
        });

        builder.MapGet("/api/machines/{id:Guid}", async (Guid id, IMachineService machineService) => {
            try
            {
                var machine = await machineService.GetMachineAsync(id);
                return Results.Ok(machine);
            } catch (Exception ex)
            {
                // ToDo: Log error
                if (ex is MachineNotFoundException)
                    return Results.NotFound();

                return Results.InternalServerError();
            }
        });

        builder.MapPatch("/api/machines/{id:Guid}/start", async (Guid id, IMachineService machineService) => {
            try {
                var result = await machineService.StartMachineAsync(new StartMachineCommandDto(id));
                return Results.Ok(result);
            } catch (Exception ex)
            {
                // ToDo: Log error
                if (ex is MachineNotFoundException)
                    return Results.NotFound();

                return Results.InternalServerError();
            }
        });

        builder.MapPatch("/api/machines/{id:Guid}/stop", async (Guid id, IMachineService machineService) => {
            try
            {
                await machineService.StopMachineAsync(id);
                return Results.NoContent();
            } catch (Exception ex)
            {
                // ToDo: Log error
                if (ex is MachineNotFoundException)
                    return Results.NotFound();
                
                return Results.InternalServerError();
            }
        });

        builder.MapPatch("/api/machines/{id:Guid}/connect", async (Guid id, IMachineService machineService) => {
            try
            {
                await machineService.ConnectMachineAsync(id);
                return Results.NoContent();
            } catch (Exception ex)
            {
                // ToDo: Log error
                if (ex is MachineNotFoundException)
                    return Results.NotFound();
                
                return Results.InternalServerError();
            }
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
