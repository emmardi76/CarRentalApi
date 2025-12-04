using CarRentalApi.Application.Services;
using CarRentalApi.Application.Services.Interfaces;
using CarRentalApi.Application.Services.ServiceInterfaces;
using CarRentalApi.Domain.Interfaces.Repository;
using CarRentalApi.Domain.Interfaces.Service;
using CarRentalApi.Domain.Services;
using CarRentalApi.Persistence;
using CarRentalApi.Persistence.Repository;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<CarRentalContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Mapster adapter configuration
builder.Services.AddMapster();
var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
typeAdapterConfig.Scan(Assembly.GetExecutingAssembly());

// IoC Container Registrations
// Register the mapper as Singleton service for my application
var mapperConfig = new Mapper(typeAdapterConfig);
builder.Services.AddSingleton<IMapper>(mapperConfig);

// Repositories
builder.Services.AddScoped<IRentalRepository, RentalRepository>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// ApplicationServices
builder.Services.AddScoped<IRentalApplicationService, RentalApplicationService>();
builder.Services.AddScoped<ICustomerApplicationService, CustomerApplicationService>();
builder.Services.AddScoped<ICarApplicationService, CarApplicationService>();

// DomainServices
builder.Services.AddScoped<IRentalDomainService, RentalDomainService>();
builder.Services.AddScoped<ICustomerDomainService, CustomerDomainService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Database creation
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CarRentalContext>();
    // This will automatically apply pending migrations    
    await dbContext.Database.MigrateAsync();
}

app.Run();
