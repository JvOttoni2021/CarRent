using FluentValidation;
using MediatR;
using CarRent.API;
using Microsoft.EntityFrameworkCore;
using CarRent.Infrastructure.DbContext;
using CarRent.Application.Services;
using CarRent.Domain.Interfaces;
using CarRent.Infraestructure.Repositories;
using CarRent.Application.Behavior;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Protected API", Version = "v1" });
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri("https://localhost:7271/connect/authorize"),
                TokenUrl = new Uri("https://localhost:7271/connect/token"),
                Scopes = new Dictionary<string, string>
            {
                {"CarRentAPI.read", "Read"},
                {"CarRentAPI.write", "Write" }
            }
            }
        }
    });
    c.OperationFilter<AuthorizeCheckOperationFilter>();
});

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

builder.Services.AddAuthentication("Bearer")
    .AddIdentityServerAuthentication("Bearer", options =>
    {
        options.Authority = "https://localhost:7271";
        options.ApiName = "CarRentAPI";
    });

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddValidatorsFromAssembly(typeof(CarRent.Application.Validators.CreateCarCommandValidator).Assembly);

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandler>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

    options.OAuthClientId("demo_api_swagger");
    options.OAuthAppName("Demo API - Swagger");
    options.OAuthUsePkce();
});

app.MapControllers();

app.Run();
