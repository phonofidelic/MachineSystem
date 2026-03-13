using System.Net;
using System.Net.Http.Json;
using MachineSystem.Application.Commands;
using MachineSystem.Application.Queries;
using MachineSystem.Application.ServiceContracts;
using MachineSystem.Application.Services.MachineService.Exceptions;

namespace MachineSystem.BlazorClient.Services;

// ToDo: Use cancellation tokens
public class MachineApiClient(IHttpClientFactory clientFactory) : IMachineApiClient
{
    private readonly HttpClient client = clientFactory.CreateClient("MachineApiClient");
    //private readonly NavigationManager navigationManager = serviceProvider.GetRequiredService<NavigationManager>();
    
    public async Task<GetMachinesResult> GetMachinesAsync(GetMachinesQuery query)
    {
        return await client.GetFromJsonAsync<GetMachinesResult>("/api/machines") ?? throw new Exception("Something went wrong");
    }

    public async Task<GetMachineResult> GetMachineAsync(GetMachineQuery query)
    {
        return await client.GetFromJsonAsync<GetMachineResult>($"/api/{query.MachineId}") ?? throw new MachineNotFoundException();
    }

    public async Task<MachineActionResult> StartMachineAsync(StartMachineCommand command)
    {
        var response = await client.PatchAsync($"/api/machines/{command.MachineId}/start", null) ?? throw new MachineNotFoundException();
        var result = await response.Content.ReadFromJsonAsync<MachineActionResult>() ?? throw new Exception("Could not read response");

        return result;
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
