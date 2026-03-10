using MachineSystem.Client.Services;
using MachineSystem.Domain.Services.MachineService;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(provider => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddScoped<IMachineService, ClientMachineService>();

await builder.Build().RunAsync();
