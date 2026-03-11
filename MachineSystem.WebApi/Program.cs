using MachineSystem.Application.Repositories;
using MachineSystem.Application.Services;
using MachineSystem.Application.Services.MachineService;
using MachineSystem.Infrastructure.Data;
using MachineSystem.Infrastructure.Services;
using MachineSystem.WebApi.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(
   options => options.UseInMemoryDatabase("MachineSystem.InMemoryDb"));

// Add services to the container
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IMachineRepository, MachineRepository>();
builder.Services.AddScoped<IMachineService, MachineService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapApiEndpoints();

app.Run();
