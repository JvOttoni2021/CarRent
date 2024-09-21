using FluentValidation;
using MediatR;
using CarRent.API;
using Microsoft.EntityFrameworkCore;
using CarRent.Infrastructure.DbContext;
using CarRent.Application.Services;
using CarRent.Domain.Interfaces;
using CarRent.Infraestructure.Repositories;
using CarRent.Application.Behavior;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<RentService>();
builder.Services.AddScoped<PaymentService>();
builder.Services.AddScoped<CarReturnedService>();

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssemblies(
        typeof(CarRent.Application.Queries.CarQueries.GetCarsQuery).Assembly,
        typeof(CarRent.Application.Commands.CarCommands.CreateCarCommand).Assembly,
        typeof(CarRent.Domain.Events.RentalCreatedEvent).Assembly,
        typeof(CarRent.Application.EventHandlers.CarReturnedEventHandler).Assembly
        );
});

builder.Services.AddAutoMapper(typeof(CarRent.Application.Mappings.MappingProfile).Assembly);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<CarRentContext>(options =>
    options.UseSqlServer(connectionString).UseLazyLoadingProxies());

builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IRentalRepository, RentalRepository>();
builder.Services.AddScoped<IPaymentReceiptRepository, PaymentReceiptRepository>();

builder.Services.AddControllers();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddValidatorsFromAssembly(typeof(CarRent.Application.Validators.CreateCarCommandValidator).Assembly);

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandler>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
