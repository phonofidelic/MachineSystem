using MachineSystem.Application.Repositories;
using MachineSystem.Application.Services;
using MachineSystem.Components;
using MachineSystem.Domain.Services;
using MachineSystem.Infrastructure.Data;
using MachineSystem.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddDbContext<ApplicationDbContext>(
   options => options.UseInMemoryDatabase("MachineSystem.InMemoryDb"));

builder.Services.AddScoped(provider => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5088")
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IMachineRepository, MachineRepository>();
builder.Services.AddScoped<IMachineService, ServerMachineService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();

    using var scope = app.Services.CreateScope();
    await Seeder.SeedDatabase(15, scope.ServiceProvider);
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(MachineSystem.Client._Imports).Assembly);

app.MapGet("/api/machines", async (IMachineRepository repository) => await repository.GetMachinesAsync());
app.MapGet("/api/machines/{id:Guid}", async (Guid id, IMachineRepository repository) => await repository.GetMachineAsync(id));
app.MapPatch("/api/machines/{id:Guid}/start", async (Guid id, IMachineService machineService) => await machineService.StartMachineAsync(id));
app.MapPatch("/api/machines/{id:Guid}/stop", async (Guid id, IMachineService machineService) => await machineService.StopMachineAsync(id));
app.MapPatch("/api/machines/{id:Guid}/connect", async (Guid id, IMachineService machineService) => await machineService.ConnectMachineAsync(id));
app.MapPatch("/api/machines/{id:Guid}/disconnect", async (Guid id, IMachineService machineService) => await machineService.DisconnectMachineAsync(id));

app.Run();
