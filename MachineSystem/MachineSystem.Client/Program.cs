using MachineSystem.Application.ServiceContracts;
using MachineSystem.BlazorClient.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

//builder.Services.AddScoped(provider => new HttpClient
//{
//    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
//});

builder.Services.AddScoped<IMachineApiClient, MachineApiClient>();

builder.Services.AddHttpClient<MachineApiClient>(client =>
    client.BaseAddress = new Uri("https://localhost:5218"));

await builder.Build().RunAsync();
