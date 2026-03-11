using System.Net;
using System.Net.Http.Json;
using MachineSystem.Application.Services.MachineService;
using MachineSystem.Application.Services.MachineService.Dtos;
using MachineSystem.Application.Services.MachineService.Exceptions;
using MachineSystem.Domain.Entities;
using Microsoft.AspNetCore.Components;

namespace MachineSystem.BlazorClient.Services;

// ToDo: Use cancellation tokens
public class ClientMachineService(IServiceProvider serviceProvider) : IMachineService
{
    private readonly HttpClient client = serviceProvider.GetRequiredService<HttpClient>();    
    private readonly NavigationManager navigationManager = serviceProvider.GetRequiredService<NavigationManager>();
    
    public async Task<List<Machine>> GetMachinesAsync()
    {
        var response = await client.GetAsync("/api/machines");
        if (response.StatusCode is HttpStatusCode.NotFound)
        {
            navigationManager.NavigateTo("/not-found");
        }
        if (response.StatusCode is not HttpStatusCode.OK) throw new Exception("Something went wrong");

        return await response.Content.ReadFromJsonAsync<List<Machine>>() ?? throw new Exception("Something went wrong");
    }

    public async Task<Machine> GetMachineAsync(Guid machineId)
    {
        var response = await client.GetAsync($"/api/{machineId}");
        if (response.StatusCode is not HttpStatusCode.OK) throw new Exception("Machine not found");
        if (response.Content is null) throw new Exception("Something went wrong");

        var content = await response.Content.ReadFromJsonAsync<Machine>();

        return content ?? throw new Exception("Something went wrong");
    }

    public async Task<StartMachineResultDto> StartMachineAsync(StartMachineCommandDto command)
    {
        var response = await client.PatchAsync($"/api/machines/{command.MachineId}/start", null, new CancellationToken());

        if (response.StatusCode != HttpStatusCode.OK) throw new MachineNotFoundException();
            
        return await response.Content.ReadFromJsonAsync<StartMachineResultDto>() ?? throw new Exception("Could not read content");
    }

    public async Task StopMachineAsync(Guid machineId)
    {
        var response = await client.PatchAsync($"/api/machines/{machineId}/stop", null);

        if (response.StatusCode is not HttpStatusCode.NoContent) throw new MachineNotFoundException();
    }

    public async Task ConnectMachineAsync(Guid machineId)
    {
        await client.PatchAsync($"/api/machines/{machineId}/connect", null);
    }

    public async Task DisconnectMachineAsync(Guid machineId)
    {
        await client.PatchAsync($"/api/machines/{machineId}/disconnect", null);
    }
}
