using CarRentalApi.Application.Dtos;
using CarRentalApi.Domain.Entities;
using Mapster;

namespace CarRentalApi.Application.Adapter;
public class CustomerMapper
{
    // Adds a static method to configure mapping
    public static void Configure()
    {
        TypeAdapterConfig<Customer, CustomerDto>.NewConfig()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
            .Map(dest => dest.LoyaltyPoints, src => src.LoyaltyPoints);              
    }
}
