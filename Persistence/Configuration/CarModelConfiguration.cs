using CarRentalApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentalApi.Persistence.Configuration;

public class CarModelConfiguration: IEntityTypeConfiguration<CarModel>
{
    public void Configure(EntityTypeBuilder<CarModel> builder)
    {
        builder.ToTable("CarModel");
        builder.HasKey(cm => cm.Id);
        builder.Property(cm => cm.Id)
               .ValueGeneratedOnAdd();
        builder.Property(cm => cm.Name)
               .IsRequired()
               .HasMaxLength(100);
        // Relationships
        builder.HasMany(cm => cm.Cars)
               .WithOne(c => c.CarModel)
               .HasForeignKey(c => c.CarModelId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            new CarModel
            {
                Id = 1,
                Name = "BMW 7"
            },
            new CarModel
            {
                Id = 2,
                Name = "Kia Sorento"
            },
            new CarModel
            {
                Id = 3,
                Name = "Nissan JuKe"
            },
            new CarModel
            {
                Id = 4,
                Name = "Seat Ibiza"
            },
            new CarModel
            {
                Id = 5,
                Name = "Audi A8"
            },
            new CarModel
            {
                Id = 6,
                Name = "Renault Clio"
            }
        );
    }
}