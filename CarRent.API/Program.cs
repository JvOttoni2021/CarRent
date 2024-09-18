using CarRent.API.Application.Persistence;
using CarRent.API.Application.Repositories;
using CarRent.API.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using MediatR;
using CarRent.API.Application.Behavior;
using CarRent.API;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

builder.Services.AddTransient<CarRentContext>();

builder.Services.AddScoped<ICarRepository, CarRepository>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services
    .AddControllers()
    .AddFluentValidation(fv => fv.DisableDataAnnotationsValidation = true);

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseExceptionHandler(opt => { });

app.UseHttpsRedirection();

app.UseAuthorization();

// Mapeamento de controladores
app.MapControllers();

app.Run();
