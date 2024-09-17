using CarRent.API.Application.Persistence;
using CarRent.API.Application.Repositories;
using CarRent.API.Domain.Commands.Requests;
using CarRent.API.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Reflection;
using FluentValidation;
using MediatR;
using CarRent.API.Application.Behavior;
using Hellang.Middleware.ProblemDetails;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

builder.Services.AddProblemDetails(opt =>
{
    opt.ExceptionDetailsPropertyName = "Detalhes da exception";
});

builder.Services.AddTransient<CarRentContext>();

builder.Services.AddControllers();

builder.Services.AddScoped<ICarRepository, CarRepository>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseProblemDetails();

app.MapControllers();

app.Run();
