using MachineSystem.Application.ServiceContracts;
using MachineSystem.BlazorClient.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddHttpClient(nameof(MachineApiClient), client =>
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddScoped<IMachineApiClient, MachineApiClient>();

await builder.Build().RunAsync();
