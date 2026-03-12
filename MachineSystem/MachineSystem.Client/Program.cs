using MachineSystem.Application.ServiceContracts;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(provider => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddHttpClient<IMachineApiClient>(client =>
    client.BaseAddress = new Uri("localhost:5218"));

await builder.Build().RunAsync();
