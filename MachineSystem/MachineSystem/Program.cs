using MachineSystem.Application.ServiceContracts;
using MachineSystem.BlazorClient.Services;
using MachineSystem.BlazorHost.Components;
using MachineSystem.BlazorHost.Endpoints;
using System.Net.Http.Headers;
using System.Net.Mime;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddScoped<ILogger, Logger<LoggerFactory>>();

builder.Services.AddHttpClient(nameof(MachineApiClient), client =>
{
    //var apiBaseUrl = builder.Configuration.GetSection(nameof(AppConfig)).Get<AppSettings>().BaseUrl
    client.BaseAddress = new Uri("http://localhost:5218");
    client.Timeout = TimeSpan.FromSeconds(20);
    client.DefaultRequestHeaders.Clear();
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
});

builder.Services.AddScoped<IMachineApiClient, MachineApiClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();

    //using var scope = app.Services.CreateScope();
    //await Seeder.SeedDatabase(15, scope.ServiceProvider);
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
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(MachineSystem.BlazorClient._Imports).Assembly);

app.MapApiClientProxyEndpoints(nameof(MachineApiClient));

if (app.Environment.IsDevelopment())
{
    app.Run("http://*:8080");
} else
{
    app.Run();
}
