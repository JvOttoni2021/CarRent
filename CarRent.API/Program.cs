using CarRent.API.Application.Persistence;
using CarRent.API.Application.Repositories;
using CarRent.API.Domain.Commands.Requests;
using CarRent.API.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

builder.Services.AddTransient<CarRentContext>();
builder.Services.AddControllers();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
