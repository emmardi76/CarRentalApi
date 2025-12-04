using CarRentalApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi.Persistence.Configuration;

public class RentalConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Rental> builder)
    {
        builder.ToTable("Rental");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id)
               .ValueGeneratedOnAdd();
        builder.Property(r => r.ExpectedDaysBooked)
               .IsRequired();
        builder.Property(r => r.ExtraDaysBooked)
               .IsRequired();
        builder.Property(r => r.StartDate)
               .IsRequired();
        builder.Property(r => r.Cost)
               .IsRequired()
               .HasColumnType("decimal(18,2)");
        builder.Property(r => r.Surcharges)
               .IsRequired()
               .HasColumnType("decimal(18,2)");
        builder.Property(r => r.ReturnedDate)
           .IsRequired(false); // nullable

        // Relationships
        builder.HasOne(r => r.Car)
               .WithMany(r => r.Rentals)
               .HasForeignKey(r => r.CarId)
               .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(r => r.Customer)
               .WithMany( r => r.Rentals)
               .HasForeignKey(r => r.CustomerId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}