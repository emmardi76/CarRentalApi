using CarRentalApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentalApi.Persistence.Configuration;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customer");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
               .ValueGeneratedOnAdd();

        builder.Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(c => c.Email)
               .IsRequired()
               .HasMaxLength(150);
        builder.HasIndex(c => c.Email)
               .IsUnique();

        builder.Property(c => c.PhoneNumber)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(c => c.LoyaltyPoints)
               .IsRequired()
               .HasDefaultValue(0);
        // Relationships
        builder.HasMany(c => c.Rentals)
               .WithOne(r => r.Customer)
               .HasForeignKey(r => r.CustomerId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            new Customer
            {
                Id = 1,
                Name = "John Doe",
                Email = "John@gmail.com",
                PhoneNumber = "123-456-7890",
                LoyaltyPoints = 0
            },
            new Customer
            {
                Id = 2,
                Name = "Jane Smith",
                Email = "Jane@gmail.com",
                PhoneNumber = "098-765-4321",
                LoyaltyPoints = 0
            },
            new Customer
            {
                Id = 3,
                Name = "Alice Johnson",
                Email = "Alice@gmail.com",
                PhoneNumber = "555-123-4567",
                LoyaltyPoints = 0
            },
            new Customer
            {
                Id = 4,
                Name = "Bob Brown",
                Email = "Bob@gmail.com",
                PhoneNumber = "444-987-6543",
                LoyaltyPoints = 0
            }
        );
    }
}