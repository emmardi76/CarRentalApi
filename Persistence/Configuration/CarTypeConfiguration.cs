using CarRentalApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentalApi.Persistence.Configuration;

public class CarTypeConfiguration : IEntityTypeConfiguration<CarType>
{
    public void Configure(EntityTypeBuilder<CarType> builder)
    {
        builder.ToTable("CarType");
        builder.HasKey(ct => ct.Id);
        builder.Property(ct => ct.Id)
               .ValueGeneratedOnAdd();
        builder.Property(ct => ct.Name)
               .IsRequired()
               .HasMaxLength(100);        
        builder.Property(ct => ct.PricePerDay)
               .IsRequired()
               .HasColumnType("decimal(18,2)");
        builder.Property(ct => ct.LoyaltyPoints)
               .IsRequired()
               .HasColumnType("int");                       

        // Relationships
        builder.HasMany(ct => ct.Cars)
               .WithOne(c => c.CarType)
               .HasForeignKey(c => c.CarTypeId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            new CarType
            {
                Id = 1,
                Name = "Premium",
                PricePerDay = 300.00m,
                LoyaltyPoints = 5
            },
            new CarType
            {
                Id = 2,
                Name = "SUV",
                PricePerDay = 150.00m,
                LoyaltyPoints = 3
            },
            new CarType
            {
                Id = 3,
                Name = "Small",
                PricePerDay = 50.00m,
                LoyaltyPoints = 1
            }
        );

    }
}