using CarRentalApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CarRentalApi.Persistence;

public class CarRentalContext : DbContext
{
    private DbContextOptions _options;    
    
    public CarRentalContext()
    {        
    }

    public CarRentalContext(DbContextOptions options) : base(options)
    {
        _options = options;
    }

    public DbSet<Car> Cars { get; set; }
    public DbSet<CarType> CarTypes { get; set; }
    public DbSet<CarModel> CarModels { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Rental> Rentals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
