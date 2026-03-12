using System.Net;
using System.Net.Http.Json;
using MachineSystem.Application.Commands;
using MachineSystem.Application.Queries;
using MachineSystem.Application.ServiceContracts;
using MachineSystem.Application.Services.MachineService.Exceptions;
using Microsoft.AspNetCore.Components;

namespace MachineSystem.BlazorClient.Services;

// ToDo: Use cancellation tokens
public class MachineApiClient(IServiceProvider serviceProvider) : IMachineApiClient
{
    private readonly HttpClient client = serviceProvider.GetRequiredService<HttpClient>();    
    private readonly NavigationManager navigationManager = serviceProvider.GetRequiredService<NavigationManager>();
    
    public async Task<GetMachinesResult> GetMachinesAsync(GetMachinesQuery query)
    {
        var response = await client.GetAsync("/api/machines");
        if (response.StatusCode is HttpStatusCode.NotFound)
        {
            navigationManager.NavigateTo("/not-found");
        }
        if (response.StatusCode is not HttpStatusCode.OK) throw new Exception("Something went wrong");

        return await response.Content.ReadFromJsonAsync<GetMachinesResult>() ?? throw new Exception("Something went wrong");
    }

    public async Task<GetMachineResult> GetMachineAsync(GetMachineQuery query)
    {
        var response = await client.GetAsync($"/api/{query.MachineId}");
        if (response.StatusCode is not HttpStatusCode.OK) throw new Exception("Machine not found");
        if (response.Content is null) throw new Exception("Something went wrong");

        var content = await response.Content.ReadFromJsonAsync<GetMachineResult>();

        return content ?? throw new Exception("Something went wrong");
    }

    public async Task<MachineActionResult> StartMachineAsync(StartMachineCommand command)
    {
        var response = await client.PatchAsync($"/api/machines/{command.MachineId}/start", null, new CancellationToken());

        if (response.StatusCode != HttpStatusCode.OK) throw new MachineNotFoundException();
            
        return await response.Content.ReadFromJsonAsync<MachineActionResult>() ?? throw new Exception("Could not read content");
    }

    public async Task<MachineActionResult> StopMachineAsync(StopMachineCommand command)
    {
        var response = await client.PatchAsync($"/api/machines/{command.MachineId}/stop", null);

        if (response.StatusCode is not HttpStatusCode.NoContent) throw new MachineNotFoundException();

        return await response.Content.ReadFromJsonAsync<MachineActionResult>() ?? throw new Exception("Could not read content");
    }

    public async Task<MachineActionResult> ConnectMachineAsync(ConnectMachineCommand command)
    {
        var response = await client.PatchAsync($"/api/machines/{command.MachineId}/connect", null);

        return await response.Content.ReadFromJsonAsync<MachineActionResult>() ?? throw new Exception("Could not read content");
    }

    public async Task<MachineActionResult> DisconnectMachineAsync(DisconnectMachineCommand command)
    {
        var response = await client.PatchAsync($"/api/machines/{command.MachineId}/disconnect", null);

        return await response.Content.ReadFromJsonAsync<MachineActionResult>() ?? throw new Exception("Could not read content");
    }
}
