using System.Text.RegularExpressions;
using MachineSystem.Application.Commands;
using MachineSystem.Application.Queries;
using MachineSystem.Application.ServiceContracts;

namespace MachineSystem.WebApi.Endpoints;

public static partial class EndpointExtensions
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder builder)
    {
        var logger = builder.ServiceProvider.GetRequiredService<ILogger<LoggerFactory>>();

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

        builder.MapPost(
            "/api/machines/create",
            async (CreateMachineCommand command, IHandler<CreateMachineCommand, CreateMachineResult> handler) =>
            {
                if (command.Name.Length < 3)
                    return Results.BadRequest(error: "Name must contain at least 3 characters");

                if (command.Name.Length > 30)
                    return Results.BadRequest(error: "Name cannot contain more than 30 characters");

                if (!AllowedMachineNameCharacters().IsMatch(command.Name))
                    return Results.BadRequest(error: "Machine name contains disallowed characters");

                logger.LogInformation("Creating new Machine");
                var result = await handler.HandleAsync(command);

                return Results.Ok(result);
            });

        builder.MapPatch(
            "/api/machines/{id:Guid}/start", 
            async (Guid id, IHandler<StartMachineCommand, MachineActionResult> handler) => 
            {
                logger.LogInformation($"Starting machine {id}");
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

        builder.MapPatch(
            "/api/machines/{id:Guid}/disconnect", 
            async (Guid id, IHandler<DisconnectMachineCommand, MachineActionResult> handler) => 
            {
                var result = await handler.HandleAsync(new DisconnectMachineCommand(id));
                return Results.Ok(result);
            });

        builder.MapDelete(
            "/api/machines/{id:Guid}/delete",
            async (Guid id, IHandler<DeleteMachineCommand, DeleteMachineResult> handler) =>
            {
                var result = await handler.HandleAsync(new DeleteMachineCommand(id));
                return Results.Ok(result);
            }
        );

        return builder;
    }

    [GeneratedRegex(@"[A-Za-z][A-Za-z0-9\-]*")]
    private static partial Regex AllowedMachineNameCharacters();
}
