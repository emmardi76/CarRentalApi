using CarRentalApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi.Persistence.Configuration;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Car> builder)
    {
        builder.ToTable("Car");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
               .ValueGeneratedOnAdd();
        builder.Property(c => c.PlateNumber)
               .IsRequired()
               .HasMaxLength(20);
        builder.HasIndex(c => c.PlateNumber)
               .IsUnique();
        builder.Property(c => c.IsRented)
               .IsRequired()
               .HasDefaultValue(false);
        // Relationships
        builder.HasOne(c => c.CarType)
               .WithMany(ct => ct.Cars)
               .HasForeignKey(c => c.CarTypeId)
               .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(c => c.CarModel)
               .WithMany(cm => cm.Cars)
               .HasForeignKey(c => c.CarModelId)
               .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(c => c.Rentals)
       .WithOne(r => r.Car)
       .HasForeignKey(r => r.CarId)
       .OnDelete(DeleteBehavior.Cascade);

        // Seed Data
        builder.HasData(
            new Car
            {
                Id = 1,
                PlateNumber = "ABC-1234",
                IsRented = false,
                CarTypeId = 1,
                CarModelId = 1
            },
            new Car
            {
                Id = 2,
                PlateNumber = "DEF-5678",
                IsRented = false,
                CarTypeId = 2,
                CarModelId = 2
            },
            new Car
            {
                Id = 3,
                PlateNumber = "GHI-9012",
                IsRented = false,
                CarTypeId = 3,
                CarModelId = 4
            },
            new Car
            {
                Id = 4,
                PlateNumber = "JKL-3456",
                IsRented = false,
                CarTypeId = 1,
                CarModelId = 5
            },
            new Car
            {
                Id = 5,
                PlateNumber = "MNO-7890",
                IsRented = false,
                CarTypeId = 2,
                CarModelId = 3
            },
            new Car
            {
                Id = 6,
                PlateNumber = "PQR-2345",
                IsRented = false,
                CarTypeId = 3,
                CarModelId = 6
            }
        );
    }
}