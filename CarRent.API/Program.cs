using CarRent.API.Domain.Interfaces;
using FluentValidation;
using MediatR;
using CarRent.API.Application.Behavior;
using CarRent.API;
using CarRent.API.Infraestructure.Persistence.Repositories;
using CarRent.API.Infraestructure.Persistence.Persistence;
using CarRent.API.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<RentService>();

builder.Services.AddScoped<PaymentService>();

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

builder.Services.AddTransient<CarRentContext>();

builder.Services.AddScoped<ICarRepository, CarRepository>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddScoped<IRentalRepository, RentalRepository>();

builder.Services.AddScoped<IPaymentReceiptRepository, PaymentReceiptRepository>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services
    .AddControllers();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseExceptionHandler(opt => { });

app.UseHttpsRedirection();

app.UseAuthorization();

// Mapeamento de controladores
app.MapControllers();

app.Run();
